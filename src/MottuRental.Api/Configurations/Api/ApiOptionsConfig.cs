using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.Commons.Extensions;

namespace MottuRental.Api.Configurations.Api;

internal static class ApiOptionsConfig
{
    public static void LoadConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var appConfig = configuration.Get<AppConfig>();

        appConfig.DbContext.ConnectionString = appConfig.DbContext.ConnectionString.DbStringFormat(configuration["DATABASE_HOST"], configuration["DATABASE_USER"], configuration["DATABASE_PASSWORD"]);

        services.AddSingleton(typeof(DbContextProvider), appConfig.DbContext);
    }
}

internal class AppConfig
{
    public DbContextProvider DbContext { get; set; } = new();
}
