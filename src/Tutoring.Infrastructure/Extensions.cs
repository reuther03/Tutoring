using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tutoring.Application;
using Tutoring.Infrastructure.Auth;
using Tutoring.Infrastructure.Database;
using Tutoring.Infrastructure.Swagger;

namespace Tutoring.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddDatabase(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        services.AddAuth(configuration);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies([
                typeof(IApplicationAssembly).Assembly,
                typeof(IInfrastructureAssembly).Assembly
            ]);
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwaggerDocumentation();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}