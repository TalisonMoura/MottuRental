using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MottuRental.Infra.CrossCutting.Commons.Providers;

namespace MottuRental.Infra.CrossCutting.Commons.Authentication.Authorization;

public class JwtUtils(JwtProvider jwtProvider) : IJwtUtils
{
    private readonly JwtProvider _jwtProvider = jwtProvider;

    public string GenerateJwtToken(Claim[] claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtProvider.JwtSecret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        });

        return tokenHandler.WriteToken(token);
    }

    public List<Claim> ValidateJwtToken(string token)
    {
        if (token is null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtProvider.JwtSecret);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        return jwtToken.Claims.ToList();
    }
}
