using Microsoft.Extensions.DependencyInjection;

namespace MottuRental.Application.MessageBroker.Configuration;

public static class MessageBrokerConfiguration
{
    public static IServiceCollection AddServiceBusConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddHostedService<MessaBrokerWorker>();

        return services;
    }
}