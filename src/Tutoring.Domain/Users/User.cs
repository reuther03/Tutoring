using Tutoring.Common.Primitives.Domain;
using Tutoring.Domain.Availabilities;
using Tutoring.Domain.Reviews;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public abstract class User : AggregateRoot<UserId>, IArchivable
{
    private readonly List<Review> _reviews = [];
    private readonly List<Availability> _availabilities = [];

    public Email Email { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public IReadOnlyList<Availability> Availabilities => _availabilities.AsReadOnly();


    public double AverageRating => _reviews.Count != 0 ? _reviews.Average(x => x.Rating) : 0;
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();

    public bool IsArchived { get; private set; }
    public DateTime? ArchivedAt { get; private set; }

    protected User()
    {
    }

    protected User(UserId id, Email email, Name firstName, Name lastName, Password password, Role role)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Role = role;
    }

    public void AddReview(Review review)
    {
        _reviews.Add(review);
    }

    public void AddAvailability(Availability availability)
    {
        _availabilities.Add(availability);
    }

    public void SetArchiveData(bool isArchived, DateTime? archivedAt)
    {
        IsArchived = isArchived;
        ArchivedAt = archivedAt;
    }
}