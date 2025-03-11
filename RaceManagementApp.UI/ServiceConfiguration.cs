using RaceManagementApp.Business.Automapper;
using RaceManagementApp.UI.Services;
using RaceManagementApp.UI.Utility.Helper;
using RaceManagementApp.UI.Utility.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceManagementApp.Business
{
    public static class ServiceConfiguration
    {
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            // Register the logger service
            services.AddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddSingleton(typeof(ILoggerService<BaseLogger>), typeof(LoggerService<BaseLogger>));

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSingleton<ObjectMapper>();
            
            // Register services within the Business layer
            services.AddScoped<UserService>();
            services.AddScoped<UserActionLogService>();
            services.AddScoped<SystemActionLogService>();
        }
    }
}
