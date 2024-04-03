using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Common;

namespace Tutoring.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetRequiredSection(DatabaseSettings.SectionName));
        var postgresOptions = configuration.GetOptions<DatabaseSettings>(DatabaseSettings.SectionName);
        services.AddDbContext<TutoringDbContext>(dbContextOptionsBuilder => { dbContextOptionsBuilder.UseNpgsql(postgresOptions.ConnectionString); });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<ITutoringDbContext, TutoringDbContext>();

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}