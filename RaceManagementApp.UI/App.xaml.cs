using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NLog;
using RaceManagementApp.Business;
using RaceManagementApp.Business.Context;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Logger;
using RaceManagementApp.UI.ViewModels;
using RaceManagementApp.UI.Views;
using System;

namespace RaceManagementApp.UI
{
    public partial class App : Application
    {
        public static IHost Host { get; private set; }
        public static Window MainAppWindow { get; private set; }
        public static Frame RootFrame { get; private set; }
        
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            this.InitializeComponent();
            AppBuilder();
            this.UnhandledException += OnUnhandledException;
            _logger.Info("Application Started.");
        }
        private static void AppBuilder()
        {
            // Initialize the host in the constructor
            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Ensure appsettings.json is loaded
                    config.SetBasePath(AppContext.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
               .ConfigureServices((context, services) =>
               {
                   // Retrieve the connection string from the configuration
                   var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                   // Register DbContext with MSSQL configuration using the connection string
                   services.AddDbContext<RaceManagementAppDbContext>(options => 
                   {
                       options.EnableSensitiveDataLogging();
                       options.EnableDetailedErrors();
                       options.UseSqlServer(connectionString);
                   });

                   // Register services from the Business project
                   services.ConfigureBusinessServices();

                   services.AddSingleton<BaseLogger>();

                   RegisterServices(services);

                   // Register all Pages
                   RegisterPages(services);

                   // Register all ViewModels
                   RegisterViewModels(services);

                   // Register MainWindow
                   services.AddSingleton<MainWindow>();

                   // Replace the line causing the error
                   //var logger = Host.Services.GetService<ILoggerService<App>>();
                   //logger?.LogInfo("Application Started - Global Logging Initialized");
               })
               .Build();
        }

        private static void RegisterViewModels(IServiceCollection services)
        {
            // Register all ViewModels here
            services.AddScoped<UserViewModel>();
            services.AddScoped<LogViewModel>();
            services.AddScoped<LoginViewModel>();
            services.AddTransient<UserActionLogViewModel>();
            services.AddTransient<SystemActionLogViewModel>();
        }

        private static void RegisterPages(IServiceCollection services)
        {
            // Register all Pages here
            services.AddSingleton<LoginPage>();
            services.AddSingleton<MainPage>();

            services.AddSingleton<UserPage>();
            services.AddSingleton<ActivityPage>();
            services.AddSingleton<UserActivityPage>();
            services.AddSingleton<SystemActivityPage>();
            //services.AddScoped<SalesAndEnquiriesPage>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<Frame>(serviceProvider =>
            {
                // Assuming you have a MainPage or a page containing the ContentFrame
                var mainPage = new MainPage(); // Make sure you get the right Frame from the page
                return mainPage.ContentFrame; // Ensure you return the actual Frame used for navigation
            });

            // Register BaseViewModel
            //services.AddTransient<BaseLogger>();

            // Register the NavigationService to be able to use the injected Frame
            services.AddScoped<NavigationService>();
            services.AddSingleton<LoaderService>();
            services.AddSingleton<UICommonService>();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // Ensure the host is started before use
            Host.Start();

            // Get MainWindow from the service provider and set it as the main window content
            MainAppWindow = new Window();
            MainAppWindow = Host.Services.GetRequiredService<MainWindow>();
            MainAppWindow.Activate();
        }

        private void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // Log the unhandled exception
            var _logger = Host.Services.GetRequiredService<ILoggerService<App>>();
            _logger.LogError("Unhandled exception occurred", e.Exception);
            // Prevent app from crashing (optional based on your preference)
            e.Handled = true;
        }
    }
}
