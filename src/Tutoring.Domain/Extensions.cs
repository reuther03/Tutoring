using Microsoft.Extensions.DependencyInjection;

namespace Tutoring.Domain;

internal static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}