using Tutoring.Common.Exceptions.Domain;
using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;

namespace Tutoring.Domain.Reviews;

public class Review : Entity<Guid>
{
    public Description Description { get; private set; } = null!;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    private Review()
    {
    }

    private Review(Guid id, Description description, int rating, DateTime createdAt) : base(id)
    {
        Description = description;
        Rating = rating;
        CreatedAt = createdAt;
    }

    public static Review Create(Description description, int rating)
    {
        if (rating is <= 0 or > 5)
            throw new DomainException("Rating must be between 1 and 5.");

        var review = new Review(Guid.NewGuid(), description, rating, DateTime.Now);
        return review;
    }
}