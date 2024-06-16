namespace MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;

public interface IMessageBrokerConsumer
{
    T GetMessage<T>(string endpoint);
}