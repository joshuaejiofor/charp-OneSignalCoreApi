using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Zeus.OneSignalCoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetupLogging(args);
            Log.Information("Starting the api host");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        private static void SetupLogging(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "local";

            var buildConfig = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                                .AddEnvironmentVariables()
                                .AddCommandLine(args)
                                .Build();
            var logDirectory = Environment.GetEnvironmentVariable("LogFilePath") ?? Directory.GetCurrentDirectory();
            if (environmentName.ToLower().Equals("prod"))
            {
                Log.Logger = new LoggerConfiguration()
                   .Enrich.WithMachineName()
                   .Enrich.FromLogContext()
                   .ReadFrom.Configuration(buildConfig)
                   .WriteTo.File(new JsonFormatter(), logDirectory + "/OneSignalCoreApi.log",
                        retainedFileCountLimit: null, fileSizeLimitBytes: null, rollingInterval: RollingInterval.Day, shared: true,
                        restrictedToMinimumLevel: LogEventLevel.Information)
                   .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                   .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                   .Enrich.WithMachineName()
                   .Enrich.FromLogContext()
                   .ReadFrom.Configuration(buildConfig)
                   .WriteTo.File(new JsonFormatter(), $@"{Directory.GetCurrentDirectory()}\Log\",
                        retainedFileCountLimit: null, fileSizeLimitBytes: null, rollingInterval: RollingInterval.Day, shared: true,
                        restrictedToMinimumLevel: LogEventLevel.Information)
                   .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                   .CreateLogger();
            }
        }

    }
}
