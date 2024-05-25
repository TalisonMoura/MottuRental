namespace MottuRental.Api;

public class Program
{
    public static void Main(string[] args) => CreateHost(args).Build().Run();

    public static IHostBuilder CreateHost(string[] args) => Host
                              .CreateDefaultBuilder(args)
                              .ConfigureAppConfiguration((hostContext, config) => { })
                              .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}

