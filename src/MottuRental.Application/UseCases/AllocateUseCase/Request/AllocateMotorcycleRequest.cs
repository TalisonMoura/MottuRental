using MottuRental.Domain.Enums;
using System.Text.Json.Serialization;
using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;
using MottuRental.Application.UseCases.AllocateUseCase.Response;

namespace MottuRental.Application.UseCases.AllocateUseCase.Request;

public record AllocateMotorcycleRequest : CommandRequest<AllocateMotorcycleResponse>
{
    [Required] public Guid DriverId { get; set; }
    [Required] public Guid MotorcycleId { get; set; }
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime DeliveryForecast { get; set; }
    [JsonIgnore] public PlanType PlanType { get; private set; }

    public AllocateMotorcycleRequest AssignType(PlanType planType)
    {
        PlanType = planType;
        return this;
    }
}