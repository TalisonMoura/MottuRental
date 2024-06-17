using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Request;

public record DeleteMotorcycleRequest : CommandRequest<bool>
{
    [Required] public Guid Id { get; private set; }

    public DeleteMotorcycleRequest AssignId(Guid id)
    {
        Id = id;
        return this;
    }
}
