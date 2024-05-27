using MottuRental.Data.UoW;
using MottuRental.Data.Repository.Base;
using MottuRental.Domain.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Infra.CrossCutting.Ioc;

public static class IocConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();

        return services;
    }
}