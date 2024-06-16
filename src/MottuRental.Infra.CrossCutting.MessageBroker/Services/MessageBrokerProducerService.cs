using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;
using MottuRental.Infra.CrossCutting.MessageBroker.Services.Base;

namespace MottuRental.Infra.CrossCutting.MessageBroker.Services;

public class MessageBrokerProducerService(
    MessageBrokerHostProvider hostProvider, 
    ILogger<MessageBrokerProducerService> logger) : MessageBrokerBaseService<MessageBrokerProducerService>(hostProvider, logger), IMessageBrokerProducer
{
    public void SendMessage(string endpoint, object message)
    {
        Channel.QueueDeclare(queue: endpoint);
        Logger.LogInformation($"Publishing message with payload {message.ToJson()}");
        Channel.BasicPublish(exchange: string.Empty, routingKey: endpoint, basicProperties: null, body: message.ToJson().ToByte());
    }
}