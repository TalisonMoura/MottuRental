using MediatR;
using Microsoft.AspNetCore.Mvc;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Application.Commons.Responses;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class MainController(IMediator mediator, IHandler<DomainNotification> notifications) : Controller
{
    protected IHandler<DomainNotification> Notifications { get; } = notifications;
    protected readonly IMediator _mediator = mediator;

    private bool IsValidOperation() => !Notifications.HasNotification();
    private BadRequestObjectResult ResponseBadRequest() => BadRequest(new InternalValidationProblemDetails(Notifications.GetErrorNotifications()));

    protected ActionResult<T> Response<T>(T response)
    {
        if (IsValidOperation())
            return response is null ? NoContent() : Ok(response);

        return ResponseBadRequest();
    }
}