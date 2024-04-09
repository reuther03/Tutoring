using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.CompetencesGroups.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;

namespace Tutoring.Application.Features.CompetencesGroups.Queries;

public record GetAllCompetenceGroupsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<CompetenceGroupDto>>
{
    internal sealed class Handler : IQueryHandler<GetAllCompetenceGroupsQuery, PaginatedList<CompetenceGroupDto>>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PaginatedList<CompetenceGroupDto>>> Handle(GetAllCompetenceGroupsQuery request, CancellationToken cancellationToken)
        {
            var competenceGroups = await _dbContext.CompetencesGroups
                .Include(x => x.Competences)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => CompetenceGroupDto.AsDto(x))
                .ToListAsync(cancellationToken);

            var totalCompetenceGroups = await _dbContext.CompetencesGroups.CountAsync(cancellationToken);

            return PaginatedList<CompetenceGroupDto>.Create(request.Page, request.PageSize, totalCompetenceGroups, competenceGroups);
        }
    }
}