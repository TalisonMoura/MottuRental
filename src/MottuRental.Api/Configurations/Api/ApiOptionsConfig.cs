using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.Commons.Extensions;

namespace MottuRental.Api.Configurations.Api;

internal static class ApiOptionsConfig
{
    public static void LoadConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var appConfig = configuration.Get<AppConfig>();

        appConfig.DbContext.ConnectionString = appConfig.DbContext.ConnectionString.DbStringFormat(configuration["DATABASE_HOST"], configuration["DATABASE_USER"], configuration["DATABASE_PASSWORD"]);
        appConfig.JwtProvider.JwtSecret = configuration["JWT_SECRET"];

        services.AddSingleton(typeof(JwtProvider), appConfig.JwtProvider);
        services.AddSingleton(typeof(DbContextProvider), appConfig.DbContext);
        services.AddSingleton(typeof(MessageBrokerHostProvider), appConfig.MessageBrokerHostSettings);
        services.AddSingleton(typeof(MessageBrokerQueuesProvider), appConfig.MessageBrokerQueuesSettings);
    }
}

internal class AppConfig
{
    public JwtProvider JwtProvider { get; set; } = new();
    public DbContextProvider DbContext { get; set; } = new();
    public MessageBrokerHostProvider MessageBrokerHostSettings { get; set; } = new();
    public MessageBrokerQueuesProvider MessageBrokerQueuesSettings { get; set; } = new();
}