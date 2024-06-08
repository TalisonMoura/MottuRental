using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Models;

public abstract class Vehicle : Entity
{
    public int Year { get; private set; }
    public string Model { get; private set; }
    public string Plate { get; private set; }
    public bool IsAllocated { get; private set; }

    public void ToAllocate(bool allocate) => IsAllocated = allocate;
    public void UpdatePlate(string updatePlate) => Plate = updatePlate;
}