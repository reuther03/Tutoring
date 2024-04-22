using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Extensions;
using Tutoring.Common.Primitives;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Features.Users.Queries.Users;

public record GetCurrentUserQuery : IQuery<UserDto>
{
    internal sealed class Handler : IQueryHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly IUserContext _userContext;
        private readonly ITutoringDbContext _dbContext;

        public Handler(IUserContext userContext, IUserRepository userRepository, ITutoringDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            //todo: myslalem na podejsciem z elseif(functional)

            var userId = _userContext.UserId;
            var userRole = _userContext.Role;
            if (userRole == Role.Student)
            {
                var student = await _dbContext.Users.OfType<Student>()
                    .Include(x => x.Subjects)
                    .Include(x => x.Reviews)
                    .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                return student is null
                    ? Result.NotFound<UserDto>("Student not found")
                    : Result.Ok(UserDto.AsStudentDto(student));
            }

            var tutor = await _dbContext.Users.OfType<Tutor>()
                .Include(x => x.CompetenceIds)
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            return tutor is null
                ? Result.NotFound<UserDto>("Tutor not found")
                : Result.Ok(UserDto.AsTutorDto(tutor));
        }
    }
}