using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public class BackOfficeUser : User
{
    private BackOfficeUser()
    {
    }

    private BackOfficeUser(UserId id, Email email, Name firstname, Name lastname, Password password)
        : base(id, email, firstname, lastname, password, Role.BackOfficeUser)
    {
    }

    public static BackOfficeUser Create(Email email, Name firstname, Name lastname, Password password)
    {
        var backOffice = new BackOfficeUser(UserId.New(), email, firstname, lastname, password);
        return backOffice;
    }
}