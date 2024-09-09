using MediatR;
using Microsoft.AspNetCore.Mvc;
using MottuRental.Api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Application.Commons.Responses;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;

namespace MottuRental.Api.Controllers;

[Authorize(Roles = "Manager")]
public class MotorcycleController(
    IMediator mediator,
    IHandler<DomainNotification> notifications) : MainController(mediator, notifications)
{

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(CreateMotorcycleResponse))]
    public async Task<ActionResult<CreateMotorcycleResponse>> PostAsync(CreateMotorcycleRequest request)
    {
        Notifications.LogInfo($"Creating a motorcycle with payload: [{request.ToJson()}]");
        return Response(await _mediator.Send(request));
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(List<GetMotorcycleByFilterResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
    public async Task<ActionResult<List<GetMotorcycleByFilterResponse>>> GetAsync([FromHeader] string? plate)
    {
        var request = new GetMotorcycleByFilterRequest().AssignPlate(plate);
        Notifications.LogInfo($"Get a motorcycle with payload: [{request.ToJson()}]");
        return Response(await _mediator.Send(request));
    }

    [HttpPut("{id}")]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
    public async Task<ActionResult<bool>> UpdateAsync(Guid id, UpdateMotorcycleRequest request)
    {
        request.AssignId(id);
        Notifications.LogInfo($"Update a motorcycle with payload: [{request.ToJson()}]");
        return Response(await _mediator.Send(request));
    }

    [HttpDelete("{id}")]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
    public async Task<ActionResult<bool>> DeleteAsync(Guid id)
    {
        Notifications.LogInfo($"Delete a motorcycle with payload: [Id: {id}]");
        return Response(await _mediator.Send(new DeleteMotorcycleRequest().AssignId(id)));
    }
}
