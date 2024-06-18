using MottuRental.Domain.Core.Messages;

namespace MottuRental.Application.UseCases.AllocateUseCase.Response;

public record AllocateMotorcycleResponse : ResponseBase
{
    public DateTime EndDate { get; set; }
    public double TotalAmount { get; set; }
}