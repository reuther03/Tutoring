using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Queries.Tutors;

public record GetTutorDetailsQuery(Guid UserId) : IQuery<TutorDetailsDto>
{
    internal sealed class Handler : IQueryHandler<GetTutorDetailsQuery, TutorDetailsDto>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TutorDetailsDto>> Handle(GetTutorDetailsQuery request, CancellationToken cancellationToken)
        {
            var tutor = await _dbContext.Users.OfType<Tutor>()
                .Include(t => t.CompetenceIds)
                .Include(x => x.Reviews)
                .Include(x => x.Availabilities)
                .FirstOrDefaultAsync(x => x.Id == Domain.Users.UserId.From(request.UserId), cancellationToken);

            return tutor is null
                ? Result.NotFound<TutorDetailsDto>("Tutor not found")
                : Result.Ok(TutorDetailsDto.AsDto(tutor));
        }
    }
}