using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Models;

public class Motorcycle(int year, string model, string plate) : Entity
{
    public int Year { get; private set; } = year;
    public string Model { get; private set; } = model;
    public string Plate { get; private set; } = plate;
}