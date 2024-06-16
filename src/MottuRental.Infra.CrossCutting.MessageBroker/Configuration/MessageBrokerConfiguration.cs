using Microsoft.Extensions.DependencyInjection;
using MottuRental.Infra.CrossCutting.MessageBroker.Services;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;

namespace MottuRental.Infra.CrossCutting.MessageBroker.Configuration;

public static class MessageBrokerConfiguration
{
    public static void AddServiceBusConfiguration(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddTransient<IMessageBrokerProducer, MessageBrokerProducerService>();
        services.AddTransient<IMessageBrokerConsumer, MessageBrokerConsumerService>();
    }
}