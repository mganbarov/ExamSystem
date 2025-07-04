using ExamSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExamDate)
                .IsRequired();

            builder.Property(x => x.ExamScore)
                .IsRequired();

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Lesson)
                .WithMany(x => x.Exams)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
