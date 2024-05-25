namespace MottuRental.Domain.Models;

public class Motorcycle : Vehicle 
{
    public virtual Allocate Allocate { get; private set; }
}