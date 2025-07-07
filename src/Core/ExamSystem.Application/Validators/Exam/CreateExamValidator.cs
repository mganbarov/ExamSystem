using ExamSystem.Application.Abstraction.Repositories;
using ExamSystem.Application.DTOs.Exam;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Validators.Exam
{
    public class CreateExamValidator : AbstractValidator<CreateExamDTO>
    {
        public CreateExamValidator()
        {
            RuleFor(x => x.StudentNumber)
                .NotEmpty()
                .WithMessage("Şagird nömrəsi boş ola bilməz");

            RuleFor(x => x.LessonCode)
                .NotEmpty()
                .WithMessage("Dərs kodu boş ola bilməz");
                
            RuleFor(x => x.ExamDate)
                .NotEmpty()
                .WithMessage("İmtahan tarixi boş ola bilməz")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("İmtahan tarixi gələcəkdə ola bilməz");

            RuleFor(x => x.ExamScore)
                .NotEmpty()
                .WithMessage("İmtahan balı boş ola bilməz");
        }

    }
}
