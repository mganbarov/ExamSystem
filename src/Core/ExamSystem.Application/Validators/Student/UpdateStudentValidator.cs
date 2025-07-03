using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Validators.Student
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .Must(x => x > 0).WithMessage("Id 0-dan böyük olmalıdır!");


            RuleFor(x => x.OrderNumber)
           .InclusiveBetween(1, 99999); // number(5,0)

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Grade)
                .InclusiveBetween(1, 11);
        }
    }
}
