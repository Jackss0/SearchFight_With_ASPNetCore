using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchFight_With_ASPNetCore.Services;
using Microsoft.Extensions.Configuration.CommandLine;

namespace SearchFight_With_ASPNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(builder =>
                {
                    builder.SetBasePath(
                        @"C:\Users\USER\source\repos\SearchFight_With_ASPNetCore\SearchFight_With_ASPNetCore");
                    builder.AddJsonFile("hostsettings.json", optional: true);
                    builder.AddEnvironmentVariables(prefix: "My_Env_Variable");
                    builder.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((context, configApp) =>
                {
                    configApp.SetBasePath(
                        @"C:\Users\USER\source\repos\SearchFight_With_ASPNetCore\SearchFight_With_ASPNetCore");
                    configApp.AddJsonFile("appsettings.json", optional: false);
                    configApp.AddEnvironmentVariables(prefix: "My_Other_Env_Variable");
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<GoogleService>();
                    services.AddScoped<IGoogleService, GoogleService>();
                    services.AddScoped<BingService>();
                    services.AddScoped<WinnerService>();
                    services.AddHttpClient();
                    
                    services.AddHostedService<MyClassListener>();
                })
                .Build();

            /*
            var googleService = host.Services.GetService<IGoogleService>();
            googleService.GoogleWinner(new[] { ".net", "java" });

            var bingService = host.Services.GetService<BingService>();
            bingService.BingWinner(new[] { ".net", "java" });*/

            //var hostValue = host.

            var cmd = new CommandLineConfigurationProvider(args);
            var cmd2 = new CommandLineConfigurationProvider(args);
            var cmd3 = new CommandLineConfigurationProvider(args);
            
            
            var winnerService = host.Services.GetService<WinnerService>();
            //winnerService.TotalWinner(new[] { ".net", "java" });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(s =>
               {
                   s.AddJsonFile("appsettings.json");
               })
               .ConfigureServices(s=> 
               {
                   
               });
        } 
    }
}