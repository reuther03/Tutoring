using Microsoft.EntityFrameworkCore;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Subjects;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Abstractions.Database;

public interface ITutoringDbContext
{
    DbSet<User> Users { get; }
    DbSet<Student> Students { get; }
    DbSet<Tutor> Tutors { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<Competence> Competences { get; }
    DbSet<CompetencesGroup> CompetencesGroups { get; }
}