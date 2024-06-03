using System.Security.Claims;

namespace MottuRental.Infra.CrossCutting.Commons.Authentication.Authorization;

public interface IJwtUtils
{
    public string GenerateJwtToken(Claim[] claims);
    public List<Claim> ValidateJwtToken(string token);
}
