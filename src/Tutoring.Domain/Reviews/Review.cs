using Tutoring.Common.Exceptions.Domain;
using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Users;

namespace Tutoring.Domain.Reviews;

public class Review : Entity<Guid>
{
    public Description Description { get; private set; } = null!;
    public int Rating { get; private set; }
    public UserId CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Review()
    {
    }

    private Review(Guid id, Description description, int rating, UserId createdBy, DateTime createdAt) : base(id)
    {
        Description = description;
        Rating = rating;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public static Review Create(Description description, int rating, UserId createdBy)
    {
        if (rating is <= 0 or > 5)
            throw new DomainException("Rating must be between 1 and 5.");

        var review = new Review(Guid.NewGuid(), description, rating, createdBy, DateTime.Now);
        return review;
    }
}