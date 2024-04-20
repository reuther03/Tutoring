using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Queries.Tutors;

public record GetTutorQuery(Guid UserId) : IQuery<TutorDto>
{
    internal sealed class Handler : IQueryHandler<GetTutorQuery, TutorDto>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TutorDto>> Handle(GetTutorQuery request, CancellationToken cancellationToken)
        {
            var tutor = await _dbContext.Users.OfType<Tutor>()
                .Include(t => t.CompetenceIds)
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == Domain.Users.UserId.From(request.UserId), cancellationToken);

            return tutor is null
                ? Result.NotFound<TutorDto>("Tutor not found")
                : Result.Ok(TutorDto.AsDto(tutor));
        }
    }
}