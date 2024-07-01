using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;
using MottuRental.Application.UseCases.AllocateUseCase.Response;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;
using MottuRental.Domain.Event;

namespace MottuRental.Application.AutoMapper;

public class AutoMapperHandler : Profile
{
    public AutoMapperHandler()
    {
        ResquetMapper();
        ResponseMapper();
    }

    public void ResquetMapper()
    {
        CreateMap<CreateDriverRequest, Driver>()
            .AfterMap((src, dest) =>
            {
                dest.SetImageName($"{src.NumeroCNH}.{src.File.FileName.Split('.')[1]}");
            });
        CreateMap<CreateMotorcycleRequest, Motorcycle>()
            .ForMember(x => x.Plate, opt => opt.MapFrom(x => x.Plate.ToUpper()));
        CreateMap<CreateMotorcycleEventByNotificationRequest, MotorcycleEvent>();
    }

    public void ResponseMapper()
    {
        CreateMap<Driver, CreateDriverReponse>();
        CreateMap<Allocate, AllocateMotorcycleResponse>();
        CreateMap<Motorcycle, GetMotorcycleByFilterResponse>();
    }
}
