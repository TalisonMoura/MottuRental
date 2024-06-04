using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Handlers;

public class GetMotorcycleByFilterUseCase(
    IMapper mapper, 
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IMotorcycleService baseService,
    IHandler<DomainNotification> notifications) : UseCaseBase<GetMotorcycleByFilterRequest, List<GetMotorcycleByFilterResponse>>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IMotorcycleService _baseService = baseService;

    public override async Task<List<GetMotorcycleByFilterResponse>> HandleSafeMode(GetMotorcycleByFilterRequest request, CancellationToken cancellationToken)
    {
        var query = request.Plate.IsNullOrWhiteSpace() ? _baseService.ExecuteQueryAsNoTracking : _baseService.ExecuteQueryAsNoTracking.Where(x => x.Plate.Equals(request.Plate));

        var response = await query.ToListAsync(cancellationToken);

        return _mapper.Map<List<GetMotorcycleByFilterResponse>>(response);
    }
}