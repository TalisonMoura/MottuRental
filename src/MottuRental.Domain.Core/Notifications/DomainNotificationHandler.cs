using Microsoft.Extensions.Logging;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Core.Notifications;

public class DomainNotificationHandler : IHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;
    private readonly ILogger<DomainNotificationHandler> _logger;
    public DomainNotificationHandler(ILogger<DomainNotificationHandler> logger)
    {
        ClearNotifications();
        _logger = logger;
    }

    public void Handle(DomainNotification args)
    {
        if (_notifications.Exists(x => string.Equals(x.Value.Trim(), args.Value.Trim(), StringComparison.OrdinalIgnoreCase)))
            _notifications.Add(args);
    }

    public List<DomainNotification> GetNotifications() => _notifications;
    public void ClearNotifications() => _notifications = [];
    public bool HasNotification() => GetNotifications().Count != 0;
    public virtual bool HasError() => GetNotifications().Exists(x => x.Type.Equals("Error"));

    public Dictionary<string, string[]> GetErrorNotifications()
    {
        var keys = _notifications.Select(s => s.Key).Distinct();
        var problemDetails = new Dictionary<string, string[]>();

        foreach (var key in keys)
            problemDetails[key] = _notifications.Where(w => w.Key.Equals(key)).Select(s => s.Value).ToArray();

        return problemDetails;
    }

    public void LogInfo(string infoMessage) => _logger.LogInformation(infoMessage);
    public void LogError(Exception ex) => _logger.LogError(ex, string.Empty);
    public void LogError(Exception ex, string errorMessage) => _logger.LogError(ex, errorMessage);

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