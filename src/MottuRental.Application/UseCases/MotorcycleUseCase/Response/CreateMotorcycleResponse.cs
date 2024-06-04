using MottuRental.Domain.Core.Messages;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Response;

public record CreateMotorcycleResponse : ResponseBase
{
    public bool IsRegistered { get; set; }
}
