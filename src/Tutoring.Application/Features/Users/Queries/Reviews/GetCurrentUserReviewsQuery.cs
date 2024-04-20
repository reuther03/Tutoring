using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;

namespace Tutoring.Application.Features.Users.Queries.Reviews;

public record GetCurrentUserReviewsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<ReviewDto>>
{
    internal sealed class Handler : IQueryHandler<GetCurrentUserReviewsQuery, PaginatedList<ReviewDto>>
    {
        private readonly ITutoringDbContext _dbContext;
        private readonly IUserContext _userContext;

        public Handler(ITutoringDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<Result<PaginatedList<ReviewDto>>> Handle(GetCurrentUserReviewsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;
            var user = await _dbContext.Users
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
                return Result.NotFound<PaginatedList<ReviewDto>>("User not found");

            var reviews = user.Reviews
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(ReviewDto.AsDto)
                .ToList();

            var totalReviews = user.Reviews.Count;
            return PaginatedList<ReviewDto>.Create(request.Page, request.PageSize, totalReviews, reviews);
        }
    }
}