using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Services.Base;

public class BaseService(IHandler<DomainNotification> notifications)
{
    protected IHandler<DomainNotification> Notifications { get; } = notifications;
}