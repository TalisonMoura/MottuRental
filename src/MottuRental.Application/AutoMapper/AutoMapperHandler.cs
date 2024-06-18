using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;
using MottuRental.Application.UseCases.MotorcycleUseCase.Request;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;
using MottuRental.Application.UseCases.AllocateUseCase.Response;

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
        CreateMap<CreateDriverRequest, Driver>();
        CreateMap<CreateMotorcycleRequest, Motorcycle>()
            .ForMember(x => x.Plate, opt => opt.MapFrom(x => x.Plate.ToUpper()));
    }

    public void ResponseMapper()
    {
        CreateMap<Driver, CreateDriverReponse>();
        CreateMap<Allocate, AllocateMotorcycleResponse>();
        CreateMap<Motorcycle, GetMotorcycleByFilterResponse>();
    }
}
