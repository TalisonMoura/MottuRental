using MottuRental.Domain.Enums;
using MottuRental.Domain.Core.Messages;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Application.UseCases.DriverUseCase.Request;

public record CreateDriverRequest : CommandRequest<CreateDriverReponse>
{
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public CnhType CnhType { get; set; }
    public string NumeroCNH { get; set; }
}