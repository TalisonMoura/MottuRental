using MottuRental.Data.Context;

namespace MottuRental.Api.Configurations.Api;

internal static class ApiConfig
{
    public static IServiceCollection ConfigureStartupApi(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.LoadConfiguration(configuration);
        services.AddDbContext<ApplicationDbContext>();
        services.AddWebApiVersioning();

        return services;
    }
}