using MottuRental.Domain.Enums;
using Microsoft.AspNetCore.Http;
using MottuRental.Domain.Core.Messages;
using System.ComponentModel.DataAnnotations;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Application.UseCases.DriverUseCase.Request;

public record CreateDriverRequest : CommandRequest<CreateDriverReponse>
{
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public string NumeroCNH { get; set; }
    public CnhType CnhType { get; private set; }
    public IFormFile File { get; private set; }

    public CreateDriverRequest(DriverParametes driver)
    {
        Name = driver.Name;
        Cnpj = driver.Cnpj;
        File = driver.File;
        CnhType = driver.CnhType;
        BirthDate = driver.BirthDate;
        NumeroCNH = driver.NumeroCNH;
    }

    public void ValidateRequest(IHandler<DomainNotification> notifications)
    {
        if (!BirthDate.HasFullAge())
            notifications.Handle(DomainNotification.Error("_006", "This driver is not full of age"));
        if (!Cnpj.IsValidCNPJ())
            notifications.Handle(DomainNotification.Error("_010", "This driver have invalid Cnpj"));
        if (!NumeroCNH.IsValidCnh())
            notifications.Handle(DomainNotification.Error("_011", "This driver have invalid Cnh"));
    }
}

public record DriverParametes
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Required]
    [Length(14, 14)]
    public string Cnpj { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    [Length(11, 11)]
    public string NumeroCNH { get; set; }

    [Required]
    public CnhType CnhType { get; set; }

    [Required]
    public IFormFile File { get; set; }
}