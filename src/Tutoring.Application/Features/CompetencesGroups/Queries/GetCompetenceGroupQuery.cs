using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.CompetencesGroups.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.CompetencesGroups.Queries;

public record GetCompetenceGroupQuery(Guid CompetencesGroupId) : IQuery<CompetenceGroupDto>
{
    internal sealed class Handler : IQueryHandler<GetCompetenceGroupQuery, CompetenceGroupDto>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<CompetenceGroupDto>> Handle(GetCompetenceGroupQuery request, CancellationToken cancellationToken)
        {
            var competencesGroup = await _dbContext.CompetencesGroups
                .Include(x => x.Competences)
                .SingleOrDefaultAsync(x => x.Id == request.CompetencesGroupId, cancellationToken);

            return competencesGroup is null
                ? Result.NotFound<CompetenceGroupDto>("Competences group not found")
                : Result.Ok(CompetenceGroupDto.AsDto(competencesGroup));
        }
    }
}