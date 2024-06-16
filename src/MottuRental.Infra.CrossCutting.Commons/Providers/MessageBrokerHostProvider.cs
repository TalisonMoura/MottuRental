namespace MottuRental.Infra.CrossCutting.Commons.Providers;

public class MessageBrokerHostProvider
{
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string HostName { get; set; }
    public string VirtualHost { get; set; }
}