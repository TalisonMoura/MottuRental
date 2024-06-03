using MottuRental.Application.AutoMapper;

namespace MottuRental.Api.Configurations.Api;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.CreateMapper();

        return services;
    }
}