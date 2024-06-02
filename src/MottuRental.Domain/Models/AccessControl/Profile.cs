using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Models.AccessControl;

public class Profile(string name) : Entity
{
    public string Name { get; private set; } = name;

    public virtual User User { get; set; }
    public virtual Driver Driver { get; set; }
}