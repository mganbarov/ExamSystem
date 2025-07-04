using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence.Context
{
    public class ApplicationDbContextIntializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public ApplicationDbContextIntializer(ApplicationDbContext context, 
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task IntializeDbContext()
        {
            await _context.Database.MigrateAsync();
        }

    }
}
