using MediatR;
using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Handlers;

public class CreateMotorcycleUseCase(
    IMapper mapper,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IMotorcycleService baseService,
    IMessageBrokerProducer messageProducer,
    IHandler<DomainNotification> notifications,
    MessageBrokerQueuesProvider producerProvider) : UseCaseBase<CreateMotorcycleRequest, CreateMotorcycleResponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IMotorcycleService _baseService = baseService;
    private readonly IMessageBrokerProducer _messageProducer = messageProducer;
    private readonly MessageBrokerQueuesProvider _producerProvider = producerProvider;

    public override async Task<CreateMotorcycleResponse> HandleSafeMode(CreateMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var entity = await _baseService.RegisterMotorcycleAsync(_mapper.Map<Motorcycle>(request), cancellationToken);

        if (entity is null)
        {
            Notifications.Handle(DomainNotification.Error("_002", $"The plate already exist"));
            return default;
        }

        if (await SaveChangesAsync() && entity.Year.Equals(2024))
            SendNotification(entity);

        return new CreateMotorcycleResponse { Id = entity.Id, IsRegistered = true };
    }

    private void SendNotification(object @event)
    {
        if (@event is Motorcycle motorcycle)
        {
            _messageProducer.SendMessage(_producerProvider.Producer.MotorcycleEvent, new { MotorcycleId = motorcycle.Id, Motorcycle = motorcycle.ToJson() });
        }
    }
}