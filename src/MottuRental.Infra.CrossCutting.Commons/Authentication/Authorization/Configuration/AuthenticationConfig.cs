using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MottuRental.Infra.CrossCutting.Commons.Providers;

namespace MottuRental.Infra.CrossCutting.Commons.Authentication.Authorization.Configuration;

public static class AuthenticationConfig
{
    public static IServiceCollection AddAuthConfiguration(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtProvider").Get<JwtProvider>().JwtSecret)),
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });
        return service;
    }
}