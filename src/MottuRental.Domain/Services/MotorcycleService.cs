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
        var entity = await ExecuteQuery.Where(x => x.Plate.Contains(motorcycle.Plate, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync(cancellationToken);

        return entity is null ? await RegisterAsync(motorcycle, cancellationToken) : default;
    }
}