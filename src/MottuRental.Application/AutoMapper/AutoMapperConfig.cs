using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace MottuRental.Application.AutoMapper;

public static class AutoMapperConfig
{
    public static IServiceCollection CreateMapper(this IServiceCollection services) =>
        services.AddSingleton(new MapperConfiguration(config =>
        {

            config.AllowNullCollections = true;
            config.AllowNullDestinationValues = true;
            config.AddProfile<AutoMapperHandler>();

        }).CreateMapper());
}