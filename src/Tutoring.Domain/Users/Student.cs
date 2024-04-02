using Tutoring.Domain.Subjects;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public sealed class Student : User
{
    private readonly List<Subject> _subjects = [];

    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();

    public Student(UserId id, Email email, Name firstname, Name lastname, Password password)
        : base(id, email, firstname, lastname, password, Role.Student)
    {
    }
}