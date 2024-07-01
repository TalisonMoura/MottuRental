using MediatR;
using Microsoft.Extensions.Hosting;
using MottuRental.Domain.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;

namespace MottuRental.Application.MessageBroker.Base;

public abstract class ConsumerBase(IServiceProvider serviceProvider, MessageBrokerQueuesProvider consumerProvider) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly MessageBrokerQueuesProvider _consumerProvider = consumerProvider;
    protected IHandler<DomainNotification> Notifications { get; set; }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var received = scope.ServiceProvider.GetRequiredService<IMessageBrokerConsumer>();
        Notifications = scope.ServiceProvider.GetRequiredService<IHandler<DomainNotification>>();

        try
        {
            await ProcessRegisterMotorcycleRequest(received, mediator);
        }
        catch (Exception ex)
        {
            Notifications.LogError(ex);
            Notifications.Handle(DomainNotification.Error("MessageBroker", ex.Message));
        }
    }

    private async Task ProcessRegisterMotorcycleRequest(IMessageBrokerConsumer received, IMediator mediator)
    {
        var request = await received.GetMessage<CreateMotorcycleEventByNotificationRequest>(_consumerProvider.Consumer.MotorcycleEvent);
        if (request is not null)
            await mediator.Send(request);
    }
}