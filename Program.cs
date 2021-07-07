using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace app_test_jmeter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                (WebHostBuilderContext context, IConfigurationBuilder builder) =>
                {
                    builder.Sources.Clear();
    
                    builder
                        .AddEnvironmentVariables()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"hiddenSettings.json", optional: false, reloadOnChange: true);
                })
                .UseStartup<Startup>();
        // public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .ConfigureAppConfiguration(
        //         (WebHostBuilderContext context, IConfigurationBuilder builder) =>
        //         {
        //             builder.Sources.Clear();
    
        //             builder
        //                 .AddEnvironmentVariables()
        //                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //                 .AddJsonFile($"hiddenSettings.json", optional: false, reloadOnChange: true);
        //         })
        //         .UseStartup<Startup>()
        //         .Build();
    }
}
