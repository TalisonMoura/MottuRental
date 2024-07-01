using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Logging;
using MottuRental.Infra.CrossCutting.Commons.Providers;

namespace MottuRental.Infra.CrossCutting.MessageBroker.Services.Base;

public abstract class MessageBrokerBase<T>
{
    protected IModel Channel { get; }
    protected ILogger<T> Logger { get; }

    private readonly IConnection _connection;
    private readonly MessageBrokerHostProvider _hostProvider;

    protected MessageBrokerBase(MessageBrokerHostProvider hostProvider, ILogger<T> logger)
    {
        Logger = logger;
        _hostProvider = hostProvider;
        _connection = new ConnectionFactory()
        {
            UserName = _hostProvider.UserName,
            Password = _hostProvider.Password,
            HostName = _hostProvider.HostName,
            VirtualHost = _hostProvider.VirtualHost
        }.CreateConnection();
        Channel = _connection.CreateModel();
    }

    protected string QueueName => Channel.QueueDeclare().QueueName;
    protected void BasicCancel(string tag) => Channel.BasicCancel(tag);
    protected void ExchangeDeclare(string exchange, string type) => Channel.ExchangeDeclare(exchange: exchange, type: type);
    protected void QueueBind(string exchange, string key) => Channel.QueueBind(queue: QueueName, exchange: exchange, routingKey: key);
    protected void BasicConsume(string queue, EventingBasicConsumer consumer) => Channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
    protected void QueueDeclare(string queue, bool durable) => Channel.QueueDeclare(queue: queue, durable: durable, exclusive: false, autoDelete: false, arguments: null);

    protected void Dispose()
    {
        if (Channel.IsOpen)
        {
            Channel.Close();
            _connection.Close();
        }
    }
}