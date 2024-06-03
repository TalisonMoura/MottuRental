using MediatR;
using Microsoft.AspNetCore.Mvc;
using MottuRental.Api.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Application.Commons.Responses;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Api.Controllers;

public class DriverController(
    IMediator mediator, 
    IHandler<DomainNotification> notifications) : MainController(mediator, notifications)
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(CreateDriverReponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
    public async Task<ActionResult<CreateDriverReponse>> PostAsync(CreateDriverRequest request)
        => Response(await _mediator.Send(request));
}