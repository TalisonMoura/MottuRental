using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;
using MottuRental.Infra.CrossCutting.MessageBroker.Services.Base;

namespace MottuRental.Infra.CrossCutting.MessageBroker.Services;

public class MessageBrokerConsumerService(
    MessageBrokerHostProvider hostProvider, 
    ILogger<MessageBrokerConsumerService> logger) : MessageBrokerBaseService<MessageBrokerConsumerService>(hostProvider, logger), IMessageBrokerConsumer
{
    public T GetMessage<T>(string endpoint)
    {
        Channel.QueueDeclare(queue: endpoint);

        string message = null;

        new EventingBasicConsumer(Channel).Received += (model, @event) =>
        {
            message = @event.Body.ToArray().GetStringFromByte();
        };

        Logger.LogInformation($"Received message from queue: {endpoint} with payload: {message}");

        return message is null ? default : message.ToObject<T>();
    }
}