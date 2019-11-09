using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchFight_With_ASPNetCore.Entities;
using SearchFight_With_ASPNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
                })
                .Build();

            /*
            var googleService = host.Services.GetService<IGoogleService>();
            googleService.GoogleWinner(new[] { ".net", "java" });

            var bingService = host.Services.GetService<BingService>();
            bingService.BingWinner(new[] { ".net", "java" });*/

            var winnerService = host.Services.GetService<WinnerService>();
            winnerService.TotalWinner(new[] { ".net", "java" });

            host.Run();
        }
    }
}