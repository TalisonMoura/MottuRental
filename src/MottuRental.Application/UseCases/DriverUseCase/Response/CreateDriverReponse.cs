using MottuRental.Domain.Core.Messages;

namespace MottuRental.Application.UseCases.DriverUseCase.Response;

public record CreateDriverReponse : ResponseBase
{
    public bool IsRegistered { get; set; }
}