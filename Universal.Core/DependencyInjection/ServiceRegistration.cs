using Microsoft.Extensions.DependencyInjection;
using Universal.Core.Interfaces;
using Universal.Core.Services;

namespace Universal.Core.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IErrorMessageService, ErrorMessageService>();

            return services;
        }
    }
}