using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.CompetencesGroups.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Exceptions.Application;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.CompetencesGroups.Queries;

public record GetCompetencesGroupQuery(Guid CompetencesGroupId) : IQuery<CompetencesGroupDto>
{
    internal sealed class Handler : IQueryHandler<GetCompetencesGroupQuery, CompetencesGroupDto>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<CompetencesGroupDto>> Handle(GetCompetencesGroupQuery request, CancellationToken cancellationToken)
        {
            var competencesGroup = await _dbContext.CompetencesGroups
                .Include(x => x.Competences)
                .SingleOrDefaultAsync(x => x.Id == request.CompetencesGroupId, cancellationToken);

            return competencesGroup is null
                ? Result.NotFound<CompetencesGroupDto>("Competences group not found")
                : Result.Ok(CompetencesGroupDto.AsDto(competencesGroup));
        }
    }
}