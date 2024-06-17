namespace MottuRental.Domain.Core.Notifications.Interfaces;

public interface IHandler<T> : IDisposable
{
    void Handle(T args);
    bool HasNotification();
    void ClearNotifications();
    List<T> GetNotifications();
    void LogInfo(string infoMessage);
    void LogError(Exception ex);
    void LogError(Exception ex, string errorMessage);
    Dictionary<string, string[]> GetErrorNotifications();
}