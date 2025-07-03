using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ExamSystem.Application
{
    public static class ServiceRegistration
    {
        public static void RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
