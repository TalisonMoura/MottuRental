using MediatR;
using Microsoft.AspNetCore.Mvc;
using MottuRental.Domain.Enums;
using MottuRental.Api.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.AllocateUseCase.Request;
using MottuRental.Application.UseCases.AllocateUseCase.Response;

namespace MottuRental.Api.Controllers;

[Authorize(Roles = "Manager, Financial, Driver")]
public class AllocateController(
    IMediator mediator, 
    IHandler<DomainNotification> notifications) : MainController(mediator, notifications)
{
    [HttpPost("{planType}")]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(AllocateMotorcycleResponse))]
    public async Task<ActionResult<AllocateMotorcycleResponse>> PostAsync(PlanType planType, AllocateMotorcycleRequest request)
    {
        request.AssignType(planType);
        Notifications.LogInfo($"Creating a motorcycle with payload: [{request.ToJson()}]");
        return Response(await _mediator.Send(request));
    }
}
