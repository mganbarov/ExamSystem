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
            RuleFor(x => x.LessonId)
            .NotEmpty()
            .Must(x => x > 0).WithMessage("Id 0-dan böyük olmalıdır!");


            RuleFor(x => x.StudentId)
                .NotEmpty()
                .Must(x=>x > 0).WithMessage("Id 0-dan böyük olmalıdır!"); 

            RuleFor(x => x.ExamDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today);

            RuleFor(x => x.ExamScore)
                .InclusiveBetween(0, 5); 
        }
    }
}
