using RabbitMQ.Client;
using Microsoft.Extensions.Logging;
using MottuRental.Infra.CrossCutting.Commons.Providers;

namespace MottuRental.Infra.CrossCutting.MessageBroker.Services.Base;

public abstract class MessageBrokerBaseService<T>
{
    protected IModel Channel {  get; }
    protected ILogger<T> Logger { get; }

    private readonly MessageBrokerHostProvider _hostProvider;

    protected MessageBrokerBaseService(MessageBrokerHostProvider hostProvider, ILogger<T> logger)
    {
        ConnectionFactory connectionFactory = new() { UserName = _hostProvider.UserName, Password = _hostProvider.Password, HostName = _hostProvider.HostName, VirtualHost = _hostProvider.VirtualHost };
        IConnection connection = connectionFactory.CreateConnection();
        Channel = connection.CreateModel();
        _hostProvider = hostProvider;
        Logger = logger;
    }
}