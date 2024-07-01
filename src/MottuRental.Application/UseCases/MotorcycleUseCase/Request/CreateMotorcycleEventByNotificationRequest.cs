using MottuRental.Domain.Core.Messages;
using MottuRental.Domain.Models;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Request;

public record CreateMotorcycleEventByNotificationRequest : CommandRequest<bool>
{
    public Guid MotorcycleId { get; set; }
    public string Motorcycle { get; set; }
}