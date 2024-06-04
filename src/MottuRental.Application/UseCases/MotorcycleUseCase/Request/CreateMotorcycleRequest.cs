using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.MotorcycleUseCase.Response;

namespace MottuRental.Application.UseCases.MotorcycleUseCase.Request;

public record CreateMotorcycleRequest : CommandRequest<CreateMotorcycleResponse>
{
    private string _model;

    [Required] public int Year { get; set; }
    [Required] public string Model { get => _model; set => _model = value.RemoveSpecialCharacters(); }
    [Required] public string Plate { get; set; }
}