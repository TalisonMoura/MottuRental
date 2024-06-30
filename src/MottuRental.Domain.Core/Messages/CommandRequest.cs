using MediatR;
using System.Text.Json.Serialization;

namespace MottuRental.Domain.Core.Messages;

public abstract record CommandRequest<TResponse> : RequestBase, IRequest<TResponse> { }

public abstract record EventRequest : RequestBase, INotification
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

public abstract record RequestBase
{
    [JsonIgnore] public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}

public abstract record ResponseBase
{
    public Guid Id { get; set; }
}