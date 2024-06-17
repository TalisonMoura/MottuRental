using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MottuRental.Api.Configurations.Swagger;

public static class SwaggerConfiguration
{
    public static void AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MottuRental.Api",
                Version = "v1",
                Description = "This is a basic project to handle the rental",
                Contact = new OpenApiContact
                {
                    Name = "MottuRental Test",
                    Url = new Uri("https://github.com/TalisonMoura/MottuRental")
                },
            });
            
            //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            c.CustomSchemaIds(x => x.FullName);
            c.AddServer(new OpenApiServer() { Url = "/motturental" });
        });
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/motturental/swagger/v1/swagger.json", "MottuRental.Api v1");
            c.RoutePrefix = "api-docs";
        });
        return app;
    }
}
