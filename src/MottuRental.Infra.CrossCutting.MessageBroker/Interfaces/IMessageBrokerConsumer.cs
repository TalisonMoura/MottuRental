namespace MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;

public interface IMessageBrokerConsumer
{
    Task<T> GetMessage<T>(string endpoint);
}