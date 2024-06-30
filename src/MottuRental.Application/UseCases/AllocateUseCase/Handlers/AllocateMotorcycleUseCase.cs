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
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.MessageBroker.Interfaces;
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
    IMessageBrokerProducer messageProducer,
    IHandler<DomainNotification> notifications,
    MessageBrokerQueuesProvider producerProvider) : UseCaseBase<AllocateMotorcycleRequest, AllocateMotorcycleResponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IAllocateService _baseService = baseService;
    private readonly IDriverService _driverService = driverService;
    private readonly IMessageBrokerProducer _messageProducer = messageProducer;
    private readonly IMotorcycleService _motorcycleService = motorcycleService;
    private readonly MessageBrokerQueuesProvider _producerProvider = producerProvider;

    public override async Task<AllocateMotorcycleResponse> HandleSafeMode(AllocateMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var (motorcycle, driver) = await GetDriverAndMotorcycleAsync(request, cancellationToken);

        if (motorcycle is not null && driver?.CnhType is CnhType.A or CnhType.AB)
        {
            var entity = new Allocate(request.DriverId, request.MotorcycleId, (int)request.PlanType, request.StartDate, request.DeliveryForecast);
            motorcycle.ToAllocate(true);

            var response = await _baseService.RegisterAsync(entity.CalculateTotalAmmout(), cancellationToken);

            await SaveChangesAsync();

            return _mapper.Map<AllocateMotorcycleResponse>(response);
        }

        Notifications.Handle(DomainNotification.Error("_005", "The motorcycle is already allocated or the driver cannot allocate without cnh A"));
        return default;
    }

    private async Task<(Motorcycle motorcycle, Driver driver)> GetDriverAndMotorcycleAsync(AllocateMotorcycleRequest request, CancellationToken cancellationToken)
    {
        var driver = await _driverService.ExecuteQueryAsNoTracking.FirstOrDefaultAsync(x => x.Id.Equals(request.DriverId), cancellationToken);
        var motorcycle = await _motorcycleService.ExecuteQuery.Where(x => !x.IsAllocated).FirstOrDefaultAsync(x => x.Id.Equals(request.MotorcycleId), cancellationToken);
        return (motorcycle, driver);
    }

    private void PublishNotification(object message)
    {
        _messageProducer.SendMessage(_producerProvider.Producer.MotorcycleEvent, message);
    }
}