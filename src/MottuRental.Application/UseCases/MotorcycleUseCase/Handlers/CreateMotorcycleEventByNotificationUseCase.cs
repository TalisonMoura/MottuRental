using MediatR;
using AutoMapper;
using MottuRental.Domain.Event;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Handlers;

public class CreateMotorcycleEventByNotificationUseCase(
    IMapper mapper,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IMotorcycleEventService baseService,
    IHandler<DomainNotification> notifications) : UseCaseBase<CreateMotorcycleEventByNotificationRequest, bool>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IMotorcycleEventService _baseService = baseService;

    public override async Task<bool> HandleSafeMode(CreateMotorcycleEventByNotificationRequest request, CancellationToken cancellationToken)
    {
        var entity = await _baseService.RegisterAsync(new MotorcycleEvent(request.MotorcycleId, request.Motorcycle), cancellationToken);

        await SaveChangesAsync();

        return entity is not null;
    }
}