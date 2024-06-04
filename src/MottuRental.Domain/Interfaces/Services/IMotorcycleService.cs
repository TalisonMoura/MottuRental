using MottuRental.Domain.Models;
using MottuRental.Domain.Interfaces.Services.Base;

namespace MottuRental.Domain.Interfaces.Services;

public interface IMotorcycleService : IBaseServiceEntity<Motorcycle>
{
    Task<Motorcycle> RegisterMotorcycleAsync(Motorcycle motorcycle, CancellationToken cancellationToken = default);
}