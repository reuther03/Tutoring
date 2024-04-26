using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Matching.Query;

public record SearchTutorQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<TutorDetailsDto>>
{
    internal sealed class Handler : IQueryHandler<SearchTutorQuery, PaginatedList<TutorDetailsDto>>
    {
        private readonly ITutoringDbContext _dbContext;
        private readonly IUserContext _userContext;
        // private IEnumerable<Tutor> _enumerable;

        public Handler(ITutoringDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            // _enumerable = enumerable;
        }


        public async Task<Result<PaginatedList<TutorDetailsDto>>> Handle(SearchTutorQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _dbContext.Users.OfType<Student>()
                .Include(x => x.Subjects)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
                return Result.NotFound<PaginatedList<TutorDetailsDto>>("User not found");

            var subjects = user.Subjects.Select(x => x.Id).ToList();


            var tutors = await _dbContext.Users.OfType<Tutor>()
                .Include(x => x.CompetenceIds)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Where(x => x.CompetenceIds.Any())
                .AsQueryable()
                .ToListAsync(cancellationToken);

            if (tutors.Count != 0)
            {
                // _enumerable =
                tutors.Where(x => x.CompetenceIds.Any(y => subjects.Contains(y))).ToList();
            }

            var totalTutors = await _dbContext.Users.OfType<Tutor>().CountAsync(cancellationToken);

            return PaginatedList<TutorDetailsDto>.Create(request.Page, request.PageSize, totalTutors, tutors.Select(TutorDetailsDto.AsDto).ToList());
        }
    }
}