using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Domain.Subjects;
using Tutoring.Domain.Users;

namespace Tutoring.Infrastructure.Database;

public class TutoringDbContext : DbContext, ITutoringDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Tutor> Tutors => Set<Tutor>();
    public DbSet<Subject> Subjects => Set<Subject>();

    public TutoringDbContext(DbContextOptions<TutoringDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}