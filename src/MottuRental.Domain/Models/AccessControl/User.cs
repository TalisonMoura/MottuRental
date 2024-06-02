using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Models.AccessControl;

public class User(string userName, string document, string password, bool isAdmin, Guid profileId) : Entity
{
    public string UserName { get; private set; } = userName;
    public string Document { get; private set; } = document;
    public string Password { get; private set; } = password;
    public bool IsAdmin { get; private set; } = isAdmin;

    public Guid ProfileId { get; private set; } = profileId;
    public virtual Profile Profile { get; set; }
}