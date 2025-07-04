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
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Grade)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.TeacherFirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.TeacherSurname)
                .IsRequired()
                .HasMaxLength(20);

        }
    }
}
