using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Models;

public class Allocate(Guid driverId, Guid motorcycleId, int allocatePeriod, DateTime startDate, DateTime endDate, DateTime deliveryForecast) : Entity
{
    public Guid DriverId { get; private set; } = driverId;
    public Guid MotorcycleId { get; private set; } = motorcycleId;
    public int AllocatePeriod { get; private set; } = allocatePeriod;

    public DateTime StartDate { get; private set; } = startDate;
    public DateTime EndDate { get; private set; } = endDate;
    public DateTime DeliveryForecast { get; private set; } = deliveryForecast;

    public virtual Driver Driver { get; private set; }
    public virtual Motorcycle Motorcycle { get; private set; }
}