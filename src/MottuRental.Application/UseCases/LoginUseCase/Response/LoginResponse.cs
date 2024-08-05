using MottuRental.Domain.Core.Messages;

namespace MottuRental.Application.UseCases.LoginUseCase.Response;

public record LoginResponse : ResponseBase
{
    public string Token { get; set; }

    public LoginResponse(string token)
    {
        Token = token;
    }
}