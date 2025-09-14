using Microsoft.EntityFrameworkCore;
using tasking_api.Infrastructure.Context;
using tasking_api.Main.Data;
using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Secure;
using tasking_api.Main.Secure.Contracts;
using tasking_api.Main.Service;
using tasking_api.Main.Service.Contracts;
using tasking_api.Main.Service.Providers;

namespace tasking_api.Infrastructure.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Repository registrations
            services.AddScoped<IBoardTaskRepository, BoardTaskRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Service registrations
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IBoardTaskService, BoardTaskService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICalendarService, CalendarService>();

            // Calendar Provider registrations
            services.AddScoped<ICalendarProvider, GoogleCalendarProvider>();
            // services.AddScoped<ICalendarProvider, OutlookCalendarProvider>(); // Add when implemented
            services.AddScoped<ICalendarProviderFactory, CalendarProviderFactory>();

            // Security registrations
            services.AddScoped<IUserSecure, UserSecure>();

            return services;
        }
    }
}
