using Meme_Oven_Data.Pages;
using Meme_Oven_Data.Repository;
using Meme_Oven_Data.Services;
using Meme_Oven_Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;

namespace Meme_Oven_Data
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // Build configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            // Setup services
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // Application initialization
            ApplicationConfiguration.Initialize();
            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext with connection string
            var connectionString = Configuration.GetConnectionString("MikrOven");
            services.AddDbContext<MicrOvenContext>(options =>
                options.UseSqlServer(connectionString)
               .EnableSensitiveDataLogging()
               .LogTo(Console.WriteLine, LogLevel.Information));

           

            // Add forms to DI
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<Form1>();
            services.AddScoped<IPLC, PLC>();
            services.AddScoped<DesOven1>();
            services.AddScoped<DesOven2>();

        }
    }
}