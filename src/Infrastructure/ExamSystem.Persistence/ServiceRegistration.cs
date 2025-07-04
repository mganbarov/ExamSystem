using ExamSystem.Application.Abstraction.Repositories;
using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Persistence.Context;
using ExamSystem.Persistence.Implementations.Repository;
using ExamSystem.Persistence.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ApplicationDbContextIntializer>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IExamService, ExamService>();

            return services;
        }
    }
}
