using MediatR;

namespace MottuRental.Domain.Core.Messages;

public abstract class CommandRequest<TResponse> : RequestBase, IRequest<TResponse> { }

public abstract class EventRequest : RequestBase, INotification
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

public abstract class RequestBase
{
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}

public abstract class ResponseBase
{
    public Guid Id { get; set; }
}