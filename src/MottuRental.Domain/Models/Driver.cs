using MottuRental.Domain.Enums;
using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Models;

public class Driver(string name, string cnpj, DateTime birthDate, CnhType cnhType, string numeroCNH, string imagemCNH) : Entity
{
    public string Name { get; private set; } = name;
    public string Cnpj { get; private set; } = cnpj;
    public DateTime BirthDate { get; private set; } = birthDate;
    public CnhType CnhType { get; private set; } = cnhType;
    public string NumeroCNH { get; private set; } = numeroCNH;
    public string ImagemCNH { get; private set; } = imagemCNH;

    public virtual Allocate Allocate {  get; private set; }
}