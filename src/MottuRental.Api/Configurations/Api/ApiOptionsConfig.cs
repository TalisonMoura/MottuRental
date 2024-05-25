namespace MottuRental.Api.Configurations.Api;

internal static class ApiOptionsConfig
{
    public static void LoadConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var appConfig = configuration.Get<AppConfig>();
    }
}

internal class AppConfig
{ 
    
}
