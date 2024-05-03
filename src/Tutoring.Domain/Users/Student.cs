using Tutoring.Domain.Subjects;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public sealed class Student : User
{
    private readonly List<Subject> _subjects = [];

    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();


    private Student()
    {
    }

    private Student(UserId id, Email email, Name firstname, Name lastname, Password password)
        : base(id, email, firstname, lastname, password, Role.Student)
    {
    }

    public static Student Create(Email email, Name firstname, Name lastname, Password password)
    {
        var student = new Student(UserId.New(), email, firstname, lastname, password);
        return student;
    }

    public void AddSubject(Subject subject)
    {
        _subjects.Add(subject);
    }
}