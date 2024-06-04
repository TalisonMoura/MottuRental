using MottuRental.Domain.Core.Messages;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Request;

public record GetMotorcycleByFilterRequest : CommandRequest<List<GetMotorcycleByFilterResponse>>
{
    public string? Plate { get; set; }
}