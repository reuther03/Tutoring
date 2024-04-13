using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Queries;

public record GetUserSubjectQuery(Guid SubjectId) : IQuery<SubjectDto>
{
    internal sealed class Handler : IQueryHandler<GetUserSubjectQuery, SubjectDto>
    {
        private readonly ITutoringDbContext _dbContext;
        private readonly IUserContext _userContext;

        public Handler(IUserContext userContext, ITutoringDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task<Result<SubjectDto>> Handle(GetUserSubjectQuery query, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _dbContext.Users.OfType<Student>()
                .Include(x => x.Subjects)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
                Result.NotFound<SubjectDto>("User not found");

            var subject = user?.Subjects.FirstOrDefault(x => x.Id == query.SubjectId);

            return subject is null
                ? Result.NotFound<SubjectDto>("Subject not found")
                : Result.Ok(SubjectDto.AsDto(subject));
        }
    }
}