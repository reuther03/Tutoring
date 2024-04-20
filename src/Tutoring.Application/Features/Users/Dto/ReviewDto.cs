using Tutoring.Domain.Reviews;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Features.Users.Dto;

public class ReviewDto
{
    public string Description { get; init; } = null!;
    public int Rating { get; init; }
    public Guid CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }

    public static ReviewDto AsDto(Review review)
    {
        return new ReviewDto
        {
            Description = review.Description,
            Rating = review.Rating,
            CreatedBy = review.CreatedBy,
            CreatedAt = review.CreatedAt
        };
    }
}