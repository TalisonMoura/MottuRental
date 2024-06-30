using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Models;

public class Allocate(Guid driverId, Guid motorcycleId, int allocatePeriod, DateTime startDate, DateTime deliveryForecast) : Entity
{
    public Guid DriverId { get; private set; } = driverId;
    public Guid MotorcycleId { get; private set; } = motorcycleId;
    public int AllocatePeriod { get; private set; } = allocatePeriod;
    public double TotalAmount { get; private set; } = SetAllocateTax(allocatePeriod);

    public DateTime StartDate { get; private set; } = startDate;
    public DateTime EndDate { get; private set; } = startDate.AddDays(allocatePeriod);
    public DateTime DeliveryForecast { get; private set; } = deliveryForecast;

    public virtual Driver Driver { get; private set; }
    public virtual Motorcycle Motorcycle { get; private set; }

    public Allocate CalculateTotalAmmout()
    {
        if (!DeliveryForecast.Date.Equals(EndDate.Date))
            if (DeliveryForecast > EndDate)
                CalculateContractAmountForLateTermination();
            else
                CalculateContractAmountForEarlyTermination();

        return this;
    }

    private static double SetAllocateTax(int allocatePediod) => allocatePediod switch
    {
        7 => 30.00 * allocatePediod,
        15 => 28.00 * allocatePediod,
        30 => 22.00 * allocatePediod,
        45 => 20.00 * allocatePediod,
        50 => 18.00 * allocatePediod,
        _ => throw new NotImplementedException()
    };

    private void CalculateContractAmountForEarlyTermination()
    {
        var notEffectiveDays = EndDate.Subtract(DeliveryForecast).Days;
        var effectiveDays = AllocatePeriod - notEffectiveDays;

        TotalAmount = AllocatePeriod switch
        {
            7 => (notEffectiveDays * 30.00 * 0.2) * effectiveDays,
            15 => (notEffectiveDays * 28.00 * 0.4) * effectiveDays,
            _ => throw new NotImplementedException()
        };
    }

    private void CalculateContractAmountForLateTermination() => TotalAmount += DeliveryForecast.Subtract(EndDate).Days * 50.00;

    public void CalculateBreachOfContractDaysAfterEndDate(int daysAfter) => TotalAmount += daysAfter * 70.00;
}