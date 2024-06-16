using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;
using MottuRental.Infra.CrossCutting.MessageBroker.Services.Base;

namespace MottuRental.Infra.CrossCutting.MessageBroker.Services;

public class MessageBrokerConsumerService(
    MessageBrokerHostProvider hostProvider,
    ILogger<MessageBrokerConsumerService> logger) : MessageBrokerBase<MessageBrokerConsumerService>(hostProvider, logger), IMessageBrokerConsumer
{
    public T GetMessage<T>(string endpoint)
    {
        try
        {
            string message = null;
            var consumer = new EventingBasicConsumer(Channel);

            consumer.Received += (model, @event) =>
            {
                message = @event.Body.ToArray().GetStringFromByte();
                Thread.Sleep(1000);
            };
            return HasMessage(message, endpoint, consumer) ? message.ToObject<T>() : default;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            return default;
        }
    }

    private bool HasMessage(string message, string endpoint, EventingBasicConsumer consumer)
    {
        if (message is not null)
        {
            BasicConsume(endpoint, consumer);
            Logger.LogInformation($"Received message from queue: {endpoint} with payload: {message}");
            return true;
        }
        return false;
    }
}