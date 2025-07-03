using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Validators.Lesson
{
    internal class UpdateLessonValidator : AbstractValidator<UpdateLessonDto>
    {
        public UpdateLessonValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty()
             .Must(x => x > 0).WithMessage("Id 0-dan böyük olmalıdır!");

            RuleFor(x => x.Code)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Grade)
                .InclusiveBetween(1, 11);

            RuleFor(x => x.TeacherFirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.TeacherLastName)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
