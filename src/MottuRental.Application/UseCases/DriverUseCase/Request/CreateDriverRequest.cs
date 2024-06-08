using MottuRental.Domain.Enums;
using System.Text.Json.Serialization;
using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Application.UseCases.DriverUseCase.Request;

public record CreateDriverRequest : CommandRequest<CreateDriverReponse>
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Required]
    [Length(14, 14)]
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }

    [JsonIgnore]
    public CnhType CnhType { get; set; }

    [Required]
    [Length(11,11)]
    public string NumeroCNH { get; set; }
}