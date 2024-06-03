using MottuRental.Api.Configurations.Api;
using MottuRental.Infra.CrossCutting.Ioc;
using MottuRental.Api.Configurations.Swagger;

namespace MottuRental.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IHostEnvironment environment)
    {
        Configuration = new ConfigurationBuilder()
                            .SetBasePath(environment.ContentRootPath)
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                            .AddEnvironmentVariables().Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        services.RegisterServices();

        services.AddAutoMapperConfiguration();

        services.ConfigureStartupApi(Configuration);

        services.AddControllers();

        services.AddHttpContextAccessor();

        services.AddSwaggerDocumentation(Configuration);

        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(Startup)));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseSwaggerDocumentation();

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(x => { x.MapDefaultControllerRoute(); });
    }
}