using MediatR;
using Microsoft.AspNetCore.Mvc;
using MottuRental.Domain.Enums;
using MottuRental.Api.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.AllocateUseCase.Request;
using MottuRental.Application.UseCases.AllocateUseCase.Response;

namespace MottuRental.Api.Controllers;

public class AllocateController(
    IMediator mediator, 
    IHandler<DomainNotification> notifications) : MainController(mediator, notifications)
{
    [HttpPost("{driverId}/{motorcycleId}/{planType}")]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(AllocateMotorcycleResponse))]
    public async Task<ActionResult<AllocateMotorcycleResponse>> PostAsync(Guid driverId, Guid motorcycleId, PlanType planType, AllocateMotorcycleRequest request)
    {
        request.AssignId(driverId, motorcycleId, planType);
        Notifications.LogInfo($"Creating a motorcycle with payload: [{request.ToJson()}]");
        return Response(await _mediator.Send(request));
    }
}
