using MediatR;
using AutoMapper;
using MottuRental.Domain.Enums;
using MottuRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.AllocateUseCase.Request;
using MottuRental.Application.UseCases.AllocateUseCase.Response;

namespace MottuRental.Application.UseCases.AllocateUseCase.Handlers;

public class AllocateMotorcycleUseCase(
    IMapper mapper, 
    IMediator mediator, 
    IUnitOfWork unitOfWork,
    IAllocateService baseService,
    IDriverService driverService,
    IMotorcycleService motorcycleService,
    IHandler<DomainNotification> notifications) : UseCaseBase<AllocateMotorcycleRequest, AllocateMotorcycleResponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IAllocateService _baseService = baseService;
    private readonly IDriverService _driverService = driverService;
    private readonly IMotorcycleService _motorcycleService = motorcycleService;

    public override async Task<AllocateMotorcycleResponse> HandleSafeMode(AllocateMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var driver = await _driverService.ExecuteQueryAsNoTracking.FirstOrDefaultAsync(x => x.Id.Equals(request.DriverId), cancellationToken);

        var isNotAllocated = await _motorcycleService.ExecuteQueryAsNoTracking.Where(x => !x.IsAllocated).FirstOrDefaultAsync(x => x.Id.Equals(request.MotorcycleId), cancellationToken);  

        if (isNotAllocated is not null && driver.CnhType is CnhType.A or CnhType.AB)
        {
            var response = await _baseService.RegisterAsync(new Allocate(request.DriverId, request.MotorcycleId, (int)request.PlanType, request.StartDate, request.DeliveryForecast), cancellationToken);

            await SaveChangesAsync();

            return _mapper.Map<AllocateMotorcycleResponse>(response);
        }

        Notifications.Handle(DomainNotification.Error("_005", "The motorcycle is already allocated or the driver cannot perform withou cnh A"));
        return default;
    }
}