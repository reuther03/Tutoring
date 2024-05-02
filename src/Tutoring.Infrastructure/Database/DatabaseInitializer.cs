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
        await SeedCompetencesMatematykaAsync(dbContext);
        await SeedCompetencesFizykaAsync(dbContext);
        await SeedCompetencesChemiaAsync(dbContext);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    // Seed competences groups

    #region Competences

    private async Task SeedCompetencesGroupAsync(TutoringDbContext dbContext)
    {
        if (dbContext.CompetencesGroups.Any())
        {
            return;
        }

        List<CompetenceGroup> competencesGroup =
        [
            CompetenceGroup.Create(new Name("Matematyka"), new Description("Grupa kompetencji związanych z matematyką.")),
            CompetenceGroup.Create(new Name("Fizyka"), new Description("Grupa kompetencji związanych z fizyką.")),
            CompetenceGroup.Create(new Name("Chemia"), new Description("Grupa kompetencji związanych z chemią."))
        ];
        await dbContext.CompetencesGroups.AddRangeAsync(competencesGroup);
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedCompetencesMatematykaAsync(TutoringDbContext dbContext)
    {
        var competencesGroup = await dbContext.CompetencesGroups.Include(competencesGroup => competencesGroup.Competences)
            .FirstAsync(x => x.Name == "Matematyka");
        if (competencesGroup.Competences.Any())
        {
            return;
        }

        var competences = new[]
        {
            Competence.Create(new Name("Dodawanie"), new Description("Umiejętność dodawania liczb.")),
            Competence.Create(new Name("Odejmowanie"), new Description("Umiejętność odejmowania liczb.")),
            Competence.Create(new Name("Mnożenie"), new Description("Umiejętność mnożenia liczb.")),
            Competence.Create(new Name("Dzielenie"), new Description("Umiejętność dzielenia liczb."))
        };


        competencesGroup.AddCompetences(competences);
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedCompetencesFizykaAsync(TutoringDbContext dbContext)
    {
        var competencesGroup = await dbContext.CompetencesGroups.Include(competencesGroup => competencesGroup.Competences).FirstAsync(x => x.Name == "Fizyka");

        if (competencesGroup.Competences.Any())
        {
            return;
        }

        var competences = new[]
        {
            Competence.Create(new Name("Prawo Ohma"),
                new Description("Prawo Ohma opisuje zależność między napięciem, natężeniem prądu i oporem elektrycznym.")),
            Competence.Create(new Name("Prawo Gaussa"), new Description("Prawo Gaussa opisuje pole elektryczne wokół ładunku elektrycznego.")),
            Competence.Create(new Name("Prawo Coulomba"), new Description("Prawo Coulomba opisuje siłę oddziaływania między dwoma ładunkami elektrycznymi."))
        };


        competencesGroup.AddCompetences(competences);
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedCompetencesChemiaAsync(TutoringDbContext dbContext)
    {
        var competencesGroup = await dbContext.CompetencesGroups.Include(competencesGroup => competencesGroup.Competences).FirstAsync(x => x.Name == "Chemia");
        if (competencesGroup.Competences.Any())
        {
            return;
        }

        var competences = new[]
        {
            Competence.Create(new Name("Reakcje chemiczne"), new Description("Umiejętność rozpoznawania reakcji chemicznych.")),
            Competence.Create(new Name("Stechiometria"), new Description("Umiejętność obliczania ilości substancji w reakcjach chemicznych.")),
            Competence.Create(new Name("Roztwory"), new Description("Umiejętność rozpoznawania i obliczania stężeń roztworów."))
        };


        competencesGroup.AddCompetences(competences);
        await dbContext.SaveChangesAsync();
    }

    #endregion

    #region Useres



    #endregion
}