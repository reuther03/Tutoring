using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Matching.Queries;

public record GetFilteredTutorsBySubjects(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<TutorDetailsDto>>
{
    internal sealed class Handler : IQueryHandler<GetFilteredTutorsBySubjects, PaginatedList<TutorDetailsDto>>
    {
        private readonly ITutoringDbContext _dbContext;
        private readonly IUserContext _userContext;

        public Handler(ITutoringDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }


        public async Task<Result<PaginatedList<TutorDetailsDto>>> Handle(GetFilteredTutorsBySubjects request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _dbContext.Users.OfType<Student>()
                .Include(x => x.Subjects)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
                return Result.NotFound<PaginatedList<TutorDetailsDto>>("User not found");

            var competenceIds = user.Subjects
                .SelectMany(x => x.CompetenceIds)
                .Select(x => x.Value)
                .Distinct().ToList();

            var query = _dbContext.Users.OfType<Tutor>()
                .Where(x => x.CompetenceIds.Any(y => competenceIds.Contains(y.Value)));


            var tutors = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);


            var totalTutors = await _dbContext.Users.OfType<Tutor>().CountAsync(cancellationToken);

            return PaginatedList<TutorDetailsDto>.Create(request.Page, request.PageSize, totalTutors, tutors.Select(TutorDetailsDto.AsDto).ToList());
        }
    }
}