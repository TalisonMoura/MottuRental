using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Request;

public record CreateMotorcycleRequest : CommandRequest<CreateMotorcycleResponse>
{
    [Required] public int Year { get; set; }
    [Required] public string Model { get; set; }

    private string _plate;
    [Required] public string Plate { get => _plate; set => _plate = value.RemoveSpecialCharacters(); }
}