using Microsoft.Extensions.DependencyInjection;
using Universal.Core.Interfaces;
using Universal.Infrastructure.Services;

namespace Universal.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, JwtTokenService>()
                .AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}