using Microsoft.Extensions.DependencyInjection;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Application.Mappings;
using TalentoPlus.Application.Services;

namespace TalentoPlus.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            
            return services;
        }
    }
}