using MottuRental.Domain.Enums;
using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;
using MottuRental.Application.UseCases.AllocateUseCase.Response;
using System.Text.Json.Serialization;

namespace MottuRental.Application.UseCases.AllocateUseCase.Request;

public record AllocateMotorcycleRequest : CommandRequest<AllocateMotorcycleResponse>
{
    [Required] public DateTime StartDate { get; set; }
    [Required] public DateTime EndDate { get; set; }
    [Required] public DateTime DeliveryForecast { get; set; }
    [JsonIgnore] public PlanType PlanType { get; set; }
    [JsonIgnore] public Guid DriverId { get; private set; }
    [JsonIgnore] public Guid MotorcycleId { get; private set; }

    public AllocateMotorcycleRequest AssignId(Guid drriverId, Guid motorcycleId)
    {
        DriverId = drriverId;
        MotorcycleId = motorcycleId;
        return this;
    }
}