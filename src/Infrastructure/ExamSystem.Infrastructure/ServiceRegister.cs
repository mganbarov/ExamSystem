using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Infrastructure.Configurations;
using ExamSystem.Infrastructure.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExamSystem.Infrastructure
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AdminCredentials>(config.GetSection("AdminCredentials"));

            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

            services.AddScoped<ITokenHandler, TokenHandler>();

            return services;
        }
    }
}
