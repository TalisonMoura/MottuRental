namespace MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;

public interface IMessageBrokerProducer
{
    void SendMessage(string endpoint, object message);
}