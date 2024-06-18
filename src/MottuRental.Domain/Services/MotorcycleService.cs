using MottuRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MottuRental.Domain.Services.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Services;

public class MotorcycleService(
    IBaseRepository<Motorcycle> baseRepository,
    IHandler<DomainNotification> notifications) : BaseServiceEntity<Motorcycle>(baseRepository, notifications), IMotorcycleService
{
    public async Task<Motorcycle> RegisterMotorcycleAsync(Motorcycle motorcycle, CancellationToken cancellationToken = default)
    {
        var entity = await ExecuteQueryAsNoTracking.AnyAsync(x => x.Plate.Contains(motorcycle.Plate), cancellationToken);

        return !entity ? await RegisterAsync(motorcycle, cancellationToken) : default;
    }
}