using MottuRental.Domain.Models;
using MottuRental.Domain.Services.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Services;

public class AllocateService(
    IBaseRepository<Allocate> baseRepository, 
    IHandler<DomainNotification> notifications) : BaseServiceEntity<Allocate>(baseRepository, notifications), IAllocateService
{

}
