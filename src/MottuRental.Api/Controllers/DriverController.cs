using MediatR;
using Microsoft.AspNetCore.Mvc;
using MottuRental.Domain.Enums;
using MottuRental.Api.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Api.Controllers;

public class DriverController(
    IMediator mediator, 
    IHandler<DomainNotification> notifications) : MainController(mediator, notifications)
{
    [HttpPost("{cnhType}")]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(CreateDriverReponse))]
    public async Task<ActionResult<CreateDriverReponse>> PostAsync(CnhType cnhType, CreateDriverRequest request)
    {
        request.AssignCnh(cnhType);
        Notifications.LogInfo($"Creating a driver with payload: {request.ToJson()}");
        return Response(await _mediator.Send(request));
    }
}