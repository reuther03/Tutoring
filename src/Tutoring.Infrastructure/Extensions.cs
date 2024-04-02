using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tutoring.Infrastructure.Swagger;

namespace Tutoring.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwaggerDocumentation();
        app.MapControllers();

        return app;
    }
}