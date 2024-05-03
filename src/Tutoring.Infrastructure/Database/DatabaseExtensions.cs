using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutoring.Application.Abstractions.Database;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Common.Extensions;
using Tutoring.Infrastructure.Database.Interceptors;
using Tutoring.Infrastructure.Database.Repository;

namespace Tutoring.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetRequiredSection(DatabaseSettings.SectionName));
        var postgresOptions = configuration.GetOptions<DatabaseSettings>(DatabaseSettings.SectionName);
        services.AddDbContext<TutoringDbContext>(dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(postgresOptions.ConnectionString);
            // dbContextOptionsBuilder.AddInterceptors()
            dbContextOptionsBuilder.AddInterceptors(new AggregateArchiveInterceptor());

        });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<ITutoringDbContext, TutoringDbContext>();

        services.AddScoped<ICompetenceGroupRepository, CompetenceGroupRepository>();
        services.AddScoped<IMatchingRepository, MatchingRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}