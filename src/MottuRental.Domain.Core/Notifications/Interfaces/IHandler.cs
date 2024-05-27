namespace MottuRental.Domain.Core.Notifications.Interfaces;

public interface IHandler<T> : IDisposable
{
    void Handle(T args);
    bool HasNotification();
    List<T> GetNotifications();
    void ClearNotifications();
}