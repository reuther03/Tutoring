using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Queries;

public record GetUserSubjectsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<SubjectDto>>
{
    internal sealed class Handler : IQueryHandler<GetUserSubjectsQuery, PaginatedList<SubjectDto>>
    {
        private readonly ITutoringDbContext _dbContext;
        private readonly IUserContext _userContext;

        public Handler(ITutoringDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<Result<PaginatedList<SubjectDto>>> Handle(GetUserSubjectsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _dbContext.Users.OfType<Student>()
                .Include(x => x.Subjects)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
                return Result.NotFound<PaginatedList<SubjectDto>>("User not found");

            var subjects = user.Subjects
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                // .Select(x => SubjectDto.AsDto(x))
                .Select(SubjectDto.AsDto)
                .ToList();

            var totalSubjects = user.Subjects.Count;

            return PaginatedList<SubjectDto>.Create(request.Page, request.PageSize, totalSubjects, subjects);
        }
    }
}