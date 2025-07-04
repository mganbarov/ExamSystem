using ExamSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence.Context
{
    public class ApplicationDbContext(DbContextOptions opt) : DbContext(opt)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        
        public DbSet<Exam> Exams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var entities = ChangeTracker.Entries<BaseEntity>();
            
            foreach(var data in entities)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.ModifiedDate = DateTime.Now;
                        break;
                }
            }



            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
