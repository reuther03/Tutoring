using Tutoring.Common.Primitives.Domain;
using Tutoring.Domain.Reviews;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public abstract class User : AggregateRoot<UserId>
{
    private readonly List<Review> _reviews = [];

    public Email Email { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public double AverageRating => _reviews.Count != 0 ? _reviews.Average(x => x.Rating) : 0;

    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();


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
}