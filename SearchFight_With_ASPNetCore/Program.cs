using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchFight_With_ASPNetCore.Services;
using System;

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

                    services.AddTransient<MyCommandLineClass>();

                    services.AddHttpClient();
                    
                    services.AddHostedService<MyClassListener>();
                })
                .Build();

            var builder2 = new ConfigurationBuilder();
            builder2.AddCommandLine(args);

            var config = builder2.Build();
            
            Console.WriteLine($"search1: '{config["search1"]}'");
            Console.WriteLine($"search2: '{config["search2"]}'");

            
            var cmdService = host.Services.GetService<MyCommandLineClass>();
            var cmdValue = cmdService.GetCommandLineValue("search1");

            var winnerService = host.Services.GetService<WinnerService>();
            host.Run();
        }
    }

    public class MyCommandLineClass
    {
        IConfiguration _config;
        public MyCommandLineClass(IConfiguration config)
        {
            _config = config;
        }

        public string GetCommandLineValue(string key)
        {
            return _config[key];
        }
    }
}