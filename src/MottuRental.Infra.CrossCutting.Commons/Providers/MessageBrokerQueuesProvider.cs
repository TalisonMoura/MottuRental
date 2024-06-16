namespace MottuRental.Infra.CrossCutting.Commons.Providers;

public class MessageBrokerQueuesProvider
{
    public ConsumerProvider Consumer { get; set; }
    public ProducerProvider Producer { get; set; }

    public class ConsumerProvider
    {
        public string MotorcycleEvent { get; set; }
    }

    public class ProducerProvider
    {
        public string MotorcycleEvent { get; set; }
    }
}