using MottuRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MottuRental.Domain.Services.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Services;

public class DriverService(
    IBaseRepository<Driver> baseRepository,
    IHandler<DomainNotification> notifications) : BaseServiceEntity<Driver>(baseRepository, notifications), IDriverService
{
    public async Task<Driver> RegisterDriverAsync(Driver driver, CancellationToken cancellationToken = default)
    {
        var entity = await ExecuteQuery.Where(x => x.Cnpj.Equals(driver.Cnpj) || x.NumeroCNH.Equals(driver.NumeroCNH) || x.Name.Equals(driver.Name)).FirstOrDefaultAsync(cancellationToken);

        return entity is null ? await RegisterAsync(driver, cancellationToken) : default;
    }
}