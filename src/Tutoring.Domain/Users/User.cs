using Tutoring.Common.Primitives.Domain;

namespace Tutoring.Domain.Users;

public record UserId(Guid Value);

public abstract class User : AggregateRoot<UserId>
{
    private User()
    {
    }

    protected User(UserId id)
        : base(id)
    {
    }
}

public sealed class Student : User
{
    public Student(UserId id) : base(id)
    {
    }
}