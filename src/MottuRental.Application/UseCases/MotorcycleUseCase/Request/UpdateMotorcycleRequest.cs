using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Request;

public record UpdateMotorcycleRequest : CommandRequest<bool>
{
    public Guid Id { get; private set; }
    [Required] public string Plate { get; set; }

    public UpdateMotorcycleRequest SetId(Guid id)
    {
        Id = id;
        return this;
    }
}
