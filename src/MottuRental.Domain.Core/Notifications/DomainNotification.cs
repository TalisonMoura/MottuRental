namespace MottuRental.Domain.Core.Notifications;

public class DomainNotification(string key, string value, string type)
{
    public string Key { get; } = key;
    public string Value { get; } = value;
    public string Type { get; } = type;

    public static DomainNotification Error(string key, string value) => new(key, value, "Error");
}
