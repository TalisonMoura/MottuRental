using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;
using MottuRental.Application.UseCases.LoginUseCase.Response;

namespace MottuRental.Application.UseCases.LoginUseCase.Request;

public record LoginRequest : CommandRequest<LoginResponse>
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
}