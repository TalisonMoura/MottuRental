using Microsoft.Extensions.DependencyInjection;
using MottuRental.Application.MessageBroker.Base;

namespace MottuRental.Application.MessageBroker.Configuration;

public static class MessageBrokerConfiguration
{
    public static IServiceCollection AddServiceBusConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddHostedService<ConsumerBase>();

        return services;
    }
}