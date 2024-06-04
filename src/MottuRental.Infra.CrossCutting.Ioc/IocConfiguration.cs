using MediatR;
using MottuRental.Data.UoW;
using MottuRental.Domain.Services;
using MottuRental.Data.Repository.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;
using MottuRental.Application.UseCases.DriverUseCase.Handlers;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;
using MottuRental.Application.UseCases.MotorcycleUseCase.Handlers;
using MottuRental.Infra.CrossCutting.Commons.Authentication.Authorization;
using System.Collections.Generic;

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
        services.AddScoped<IDriverService, DriverService>();
        services.AddScoped<IMotorcycleService, MotorcycleService>();


        services.AddScoped<IRequestHandler<CreateDriverRequest, CreateDriverReponse>, CreateDriverUseCase>();
        services.AddScoped<IRequestHandler<CreateMotorcycleRequest, CreateMotorcycleResponse>, CreateMotorcycleUseCase>();
        services.AddScoped<IRequestHandler<GetMotorcycleByFilterRequest, List<GetMotorcycleByFilterResponse>>, GetMotorcycleByFilterUseCase>();

        return services;
    }
}