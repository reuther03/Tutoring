using Tutoring.Common.Primitives.Domain;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public abstract class User : AggregateRoot<UserId>
{
    public Email Email { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }

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
}