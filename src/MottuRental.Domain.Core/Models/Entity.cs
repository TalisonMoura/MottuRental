namespace MottuRental.Domain.Core.Models;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; }
    public bool IsDeleted { get; protected set; }

    public void Delete() => IsDeleted = true;
    public void UpdatedAtNow() => UpdatedAt = DateTime.UtcNow;

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null) return false;
        if (a is null || b is null) return true;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);
    public override int GetHashCode() => (GetType().GetHashCode() * 493) + Id.GetHashCode();
    public override string ToString() => $"{GetType().Name} - [Id = {Id}]";
}
