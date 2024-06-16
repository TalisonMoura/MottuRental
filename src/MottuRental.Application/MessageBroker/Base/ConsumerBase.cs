using MediatR;
using MottuRental.Domain.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;

namespace MottuRental.Application.MessageBroker.Base;

public abstract class ConsumerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly MessageBrokerQueuesProvider.ConsumerProvider _consumerProvider;
    protected IHandler<DomainNotification> Notifications { get; set; }

    protected ConsumerBase(IServiceProvider serviceProvider, MessageBrokerQueuesProvider consumerProvider)
    {
        _serviceProvider = serviceProvider;
        _consumerProvider = consumerProvider.Consumer;
    }

    protected async Task Consume()
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
        var request = received.GetMessage<CreateMotorcycleRequest>(_consumerProvider.MotorcycleEvent);
        if (request is not null)
            await mediator.Send(request);
    }
}
