using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;

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
    }

    public void ResponseMapper()
    {
        CreateMap<Driver, CreateDriverReponse>();
    }
}
