using MediatR;
using Microsoft.AspNetCore.Mvc;
using MottuRental.Api.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.LoginUseCase.Request;
using MottuRental.Application.UseCases.LoginUseCase.Response;

namespace MottuRental.Api.Controllers;

public class UserController(
    IMediator mediator, 
    IHandler<DomainNotification> notifications) : MainController(mediator, notifications)
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, null, typeof(LoginResponse))]
    public async Task<ActionResult<LoginResponse>> PostAsync(LoginRequest request)
    {
        Notifications.LogInfo($"Login with payload: [{request.UserName}]");
        return Response(await _mediator.Send(request));
    }
}