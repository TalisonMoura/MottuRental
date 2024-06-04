using MottuRental.Domain.Models;
using MottuRental.Domain.Interfaces.Services.Base;

namespace MottuRental.Domain.Interfaces.Services;

public interface IDriverService : IBaseServiceEntity<Driver>
{
    Task<Driver> RegisterDriverAsync(Driver driver, CancellationToken cancellationToken = default);
}