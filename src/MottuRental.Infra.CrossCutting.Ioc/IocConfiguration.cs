using MediatR;
using MottuRental.Data.UoW;
using MottuRental.Data.Repository.Base;
using MottuRental.Domain.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;
using MottuRental.Application.UseCases.DriverUseCase.Handlers;
using MottuRental.Infra.CrossCutting.Commons.Authentication.Authorization;

namespace MottuRental.Infra.CrossCutting.Ioc;

public static class IocConfiguration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        RegisterInfraService(services);
        RegisterDomainService(services);

        return services;
    }

    public static IServiceCollection RegisterInfraService(IServiceCollection services)
    {
        services.AddTransient<IJwtUtils, JwtUtils>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();

        return services;
    }

    public static IServiceCollection RegisterDomainService(IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateDriverRequest, CreateDriverReponse>, CreateDriverUseCase>();

        return services;
    }
}