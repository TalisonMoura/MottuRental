using MottuRental.Domain.Services.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Models.AccessControl;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Services;

public class UserService(
    IBaseRepository<User> baseRepository, 
    IHandler<DomainNotification> notifications) : BaseServiceEntity<User>(baseRepository, notifications), IUserService
{

}
