using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Event;

public class MotorcycleEvent(Guid motorcycleId, string motorcycle) : Entity
{
    public Guid MotorcycleId { get; private set; } = motorcycleId;
    public string Motorcycle { get; private set; } = motorcycle;
}