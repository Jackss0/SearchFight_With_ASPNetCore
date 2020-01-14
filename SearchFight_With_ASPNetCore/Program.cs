using System;
using System.Linq;
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
                        @"D:\Repos\SearchFight_With_ASPNetCore_2\SearchFight_With_ASPNetCore");
                    builder.AddJsonFile("hostsettings.json", optional: true);
                    builder.AddEnvironmentVariables(prefix: "My_Env_Variable");
                    builder.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((context, configApp) =>
                {
                    configApp.SetBasePath(
                        @"D:\Repos\SearchFight_With_ASPNetCore_2\SearchFight_With_ASPNetCore");
                    configApp.AddJsonFile("appsettings.json", optional: false);
                    configApp.AddEnvironmentVariables(prefix: "My_Other_Env_Variable");
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IOperationTransient, Operation>();
                    services.AddScoped<IOperationScoped, Operation>();
                    services.AddSingleton<IOperationSingleton, Operation>();
                    services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

                    services.AddTransient<GoogleService>();
                    services.AddScoped<IGoogleService, GoogleService>();
                    services.AddScoped<BingService>();
                    services.AddScoped<WinnerService>();

                    services.AddTransient<MyCommandLineClass>();

                    services.AddHttpClient();
                    
                    services.AddHostedService<MyClassListener>();
                })
                .Build();
            
            var winnerService = host.Services.GetService<WinnerService>();
            winnerService.TotalWinner(args);
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