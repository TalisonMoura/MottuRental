using MottuRental.Domain.Event;
using MottuRental.Domain.Services.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Services;

public class MotorcycleEventService(
    IHandler<DomainNotification> notifications,
    IBaseRepository<MotorcycleEvent> baseRepository) : BaseServiceEntity<MotorcycleEvent>(baseRepository, notifications), IMotorcycleEventService
{
}