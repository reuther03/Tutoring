using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TutoringDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        // Seed data
        await SeedCompetencesGroupAsync(dbContext);
        // await SeedCompetencesAsync(dbContext);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    // Seed competences groups
    private async Task SeedCompetencesGroupAsync(TutoringDbContext dbContext)
    {
        if (dbContext.CompetencesGroups.Any())
        {
            // Data already exists, no need to seed again.
            return;
        }

        List<CompetencesGroup> competencesGroup =
        [
            CompetencesGroup.Create(new Name("Matematyka"), new Description("Grupa kompetencji związanych z matematyką.")),
            CompetencesGroup.Create(new Name("Fizyka"), new Description("Grupa kompetencji związanych z fizyką.")),
            CompetencesGroup.Create(new Name("Chemia"), new Description("Grupa kompetencji związanych z chemią."))
        ];
        await dbContext.CompetencesGroups.AddRangeAsync(competencesGroup);
        await dbContext.SaveChangesAsync();
    }

    // private async Task SeedCompetencesAsync(TutoringDbContext dbContext)
    // {
    //     var competences = new[]
    //     {
    //         Competence.Create(new Name("Dodawanie"), new Description("Umiejętność dodawania liczb.")),
    //         Competence.Create(new Name("Odejmowanie"), new Description("Umiejętność odejmowania liczb.")),
    //         Competence.Create(new Name("Mnożenie"), new Description("Umiejętność mnożenia liczb.")),
    //         Competence.Create(new Name("Dzielenie"), new Description("Umiejętność dzielenia liczb."))
    //     };
    //
    //     var competencesGroup = await dbContext.CompetencesGroups.FirstAsync();
    //     competencesGroup.AddCompetences(competences);
    //     await dbContext.SaveChangesAsync();
    // }
}