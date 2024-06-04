using MottuRental.Domain.Core.Messages;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Response;

public record GetMotorcycleByFilterResponse : ResponseBase
{
    public int Year { get; set; }
    public string Model { get; set; }
    public string Plate { get; set; }
    public bool IsAllocated { get; set; }
}