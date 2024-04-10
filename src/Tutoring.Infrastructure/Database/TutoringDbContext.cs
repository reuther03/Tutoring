using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;

namespace Tutoring.Infrastructure.Database;

public class TutoringDbContext : DbContext, ITutoringDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<CompetenceGroup> CompetencesGroups => Set<CompetenceGroup>();

    public TutoringDbContext(DbContextOptions<TutoringDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}