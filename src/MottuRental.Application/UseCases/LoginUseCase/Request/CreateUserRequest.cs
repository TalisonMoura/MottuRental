using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace MottuRental.Application.UseCases.LoginUseCase.Request;

public record CreateUserRequest : CommandRequest<bool>
{
    [Required] public string UserName { get; set; }
    [Required] public string Document { get; set; }
    [Required] public string Password { get; set; }
    [Required][Compare("Password")] public string RePassword { get; set; }
    public bool IsAdmin { get; set; } = false;
}