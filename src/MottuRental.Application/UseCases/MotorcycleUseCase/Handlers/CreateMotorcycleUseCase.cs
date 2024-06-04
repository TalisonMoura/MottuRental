using MediatR;
using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Handlers;

public class CreateMotorcycleUseCase(
    IMapper mapper,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IMotorcycleService baseService,
    IHandler<DomainNotification> notifications) : UseCaseBase<CreateMotorcycleRequest, CreateMotorcycleResponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IMotorcycleService _baseService = baseService;

    public override async Task<CreateMotorcycleResponse> HandleSafeMode(CreateMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var entity = await _baseService.RegisterMotorcycleAsync(_mapper.Map<Motorcycle>(request), cancellationToken);

        if (entity is null)
        {
            Notifications.Handle(DomainNotification.Error("_002", $"The plate: [{request.Plate}] already exists"));
            return default;
        }

        await SaveChangesAsync();

        return new CreateMotorcycleResponse { Id = entity.Id, IsRegistered = true };
    }
}