using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public sealed class Tutor : User
{
    public Tutor(UserId id, Email email, Name firstname, Name lastname, Password password)
        : base(id, email, firstname, lastname, password, Role.Tutor)
    {
    }
}