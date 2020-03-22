using System;
using System.Net;
using Newtonsoft.Json;
using System.Web;
using WSDOT_API_Project.Utilities;
using System.Net.Http;
using System.Threading.Tasks;
using WSDOT_API_Project.Models;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using WSDOT_API_Project.Interfaces;
using WSDOT_API_Project.HTTP;

namespace WSDOT_API_Project
{
    internal class Program
    {
        private static IConfiguration configuration { get; set; }
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            // runs the App.cs part
            await serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection services) 
        {
            // Builds configuration and adds json file
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",optional: true, reloadOnChange:true)
                .Build();

            // adds optional app settings
            services.AddOptions();
            //services.Configure<AppSettings>(configuration.GetSection("apiKey"));
            services.Configure<AppSettings>(x =>configuration.GetSection("apiKey"));

            // add access to Iconfiguration
            services.AddSingleton<IConfiguration>(configuration);

            // configure services and maps interface to actual HTTP store that it goes to
            services.AddTransient<IWSDOTStore, WSDOTStore>();

            // IMPORTANT! REGISTER OUR APPLICATION ENTRY POINT
            services.AddTransient<App>();
        }
    }
}
