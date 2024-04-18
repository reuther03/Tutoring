using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Features.Users.Dto;
using Tutoring.Common.Abstractions;
using Tutoring.Common.Primitives;
using Tutoring.Common.Primitives.Pagination;

namespace Tutoring.Application.Features.Users.Queries.Reviews;

public record GetUserReviewsQuery(Guid UserId, int Page = 1, int PageSize = 10) : ICommand<PaginatedList<ReviewDto>>
{
    internal sealed class Handler : ICommandHandler<GetUserReviewsQuery, PaginatedList<ReviewDto>>
    {
        private readonly ITutoringDbContext _dbContext;

        public Handler(ITutoringDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PaginatedList<ReviewDto>>> Handle(GetUserReviewsQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync(x => x.Id == Domain.Users.UserId.From(request.UserId), cancellationToken);

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