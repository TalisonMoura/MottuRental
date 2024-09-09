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
    public async Task<T> GetMessage<T>(string endpoint)
    {
        try
        {
            var consumer = new EventingBasicConsumer(Channel);
            var receivedMessage = new TaskCompletionSource<string>();

            consumer.Received += (model, @event) =>
            {
                if (!receivedMessage.Task.IsCompleted)
                    receivedMessage.SetResult(@event.Body.ToArray().GetStringFromByte());
            };
            BasicConsume(endpoint, consumer);

            var message = await receivedMessage.Task;
            return HasMessage(message, endpoint) ? message.ToObject<T>() : default;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private bool HasMessage(string message, string endpoint)
    {
        if (!message.IsNullOrWhiteSpace())
        {
            Logger.LogInformation($"Received message from queue: [{endpoint}] with payload: [{message}]");
            return true;
        }
        return false;
    }
}