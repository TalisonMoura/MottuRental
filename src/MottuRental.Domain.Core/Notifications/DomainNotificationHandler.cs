using Microsoft.Extensions.Logging;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Core.Notifications;

public class DomainNotificationHandler : IHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;
    private readonly ILogger<DomainNotificationHandler> _logger;
    public DomainNotificationHandler() => ClearNotifications();

    public void Handle(DomainNotification args)
    {
        if (_notifications.Exists(x => string.Equals(x.Value.Trim(), args.Value.Trim(), StringComparison.OrdinalIgnoreCase)))
            _notifications.Add(args);
    }

    public List<DomainNotification> GetNotifications() => _notifications;
    public void ClearNotifications() => _notifications = [];
    public bool HasNotification() => GetNotifications().Count != 0;
    public virtual bool HasError() => GetNotifications().Exists(x => x.Type.Equals("Error"));

    protected virtual void Dispose(bool disposing)
    {
        _notifications = null;
        ClearNotifications();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}