using Microsoft.EntityFrameworkCore;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Subjects;
using Tutoring.Domain.Users;

namespace Tutoring.Application.Abstractions.Database;

public interface ITutoringDbContext
{
    DbSet<User> Users { get; }
    DbSet<CompetenceGroup> CompetencesGroups { get; }
}