using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Exceptions.Application;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.CompetencesGroups.Queries;

public record GetCompetencesGroupQuery(Guid CompetencesGroupId) : IQuery<Result<CompetencesGroupDto>>
{
    internal sealed class Handler : IQueryHandler<GetCompetencesGroupQuery, Result<CompetencesGroupDto>>
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

            if (competencesGroup is null)
                throw new ApplicationValidationException("Competences group not found");

            return Result<CompetencesGroupDto>.Ok(CompetencesGroupDto.AsDto(competencesGroup));
        }
    }
}