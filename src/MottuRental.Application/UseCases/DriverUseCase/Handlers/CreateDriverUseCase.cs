using MediatR;
using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Application.UseCases.DriverUseCase.Handlers;

public class CreateDriverUseCase(
    IMapper mapper,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IDriverService driverService,
    IHandler<DomainNotification> notifications) : UseCaseBase<CreateDriverRequest, CreateDriverReponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IDriverService _driverService = driverService;

    public override async Task<CreateDriverReponse> HandleSafeMode(CreateDriverRequest request, CancellationToken cancellationToken)
    {
        var response = await _driverService.RegisterDriverAsync(_mapper.Map<Driver>(request), cancellationToken);

        if (response is null)
        {
            Notifications.Handle(DomainNotification.Error("_001", "This driver has already exists"));
            return default;
        }

        await SaveChangesAsync();

        return new CreateDriverReponse { Id = response.Id, IsRegistered = true };
    }
}