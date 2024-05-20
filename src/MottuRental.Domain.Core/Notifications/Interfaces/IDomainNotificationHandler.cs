namespace MottuRental.Domain.Core.Notifications.Interfaces;

public interface IDomainNotificationHandler<T> : IDisposable
{
    void Handle(T args);
    bool HasNotification();
    List<T> GetNotifications();
    void ClearNotifications();
}
