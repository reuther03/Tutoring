using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;

namespace Tutoring.Application.Features.Users.Queries.Users;

public record GetCurrentUserQuery : IQuery<UserDto>
{
    internal sealed class Handler : IQueryHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly IUserContext _userContext;
        private readonly ITutoringDbContext _dbContext;

        public Handler(IUserContext userContext, ITutoringDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var user = await _dbContext.Users
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            return user is null
                ? Result.NotFound<UserDto>("Student not found")
                : Result.Ok(UserDto.AsDto(user));
        }
    }
}