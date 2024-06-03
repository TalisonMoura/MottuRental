using MottuRental.Domain.Core.Messages;

namespace MottuRental.Application.UseCases.DriverUseCase.Response;

public class CreateDriverReponse : ResponseBase
{
    public bool IsRegistered { get; set; }
}