using MediatR;
using AutoMapper;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Handlers;

public class UpdateMotorcycleUseCase(
    IMapper mapper,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IMotorcycleService baseService,
    IHandler<DomainNotification> notifications) : UseCaseBase<UpdateMotorcycleRequest, bool>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IMotorcycleService _baseService = baseService;

    public override async Task<bool> HandleSafeMode(UpdateMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var entity = await _baseService.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            Notifications.Handle(DomainNotification.Error("_003", "This motorcycle doesn't exist"));
            return default;
        }

        entity.UpdatePlate(request.Plate);

        return await SaveChangesAsync();
    }
}