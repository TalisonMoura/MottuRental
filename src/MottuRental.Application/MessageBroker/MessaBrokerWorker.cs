using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using MottuRental.Application.MessageBroker.Base;
using MottuRental.Infra.CrossCutting.Commons.Providers;

namespace MottuRental.Application.MessageBroker;

public class MessaBrokerWorker(
    IServiceProvider serviceProvider,
    ILogger<MessaBrokerWorker> logger,
    MessageBrokerQueuesProvider consumerProvider) : ConsumerBase(serviceProvider, consumerProvider), IHostedService
{
    private readonly ILogger<MessaBrokerWorker> _logger = logger;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Executing process {DateTime.UtcNow}");
        _ = Consume();
        return StopAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}