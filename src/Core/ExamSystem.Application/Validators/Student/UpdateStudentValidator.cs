using ExamSystem.Application.Abstraction.Repositories;
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
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id 0-dan böyük olmalıdır!");

            RuleFor(x => x.OrderNumber)
                .InclusiveBetween(1, 99999)
                .WithMessage("Şagird nömrəsi 1-99999 aralığında olmalıdır");
                

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Şagirdin adı boş ola bilməz")
                .MaximumLength(30)
                .WithMessage("Şagirdin adı 30 simvoldan çox ola bilməz");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Şagirdin soyadı boş ola bilməz")
                .MaximumLength(30)
                .WithMessage("Şagirdin soyadı 30 simvoldan çox ola bilməz");

            RuleFor(x => x.Grade)
                .InclusiveBetween(1, 11)
                .WithMessage("Sinif 1-11 aralığında olmalıdır");
        }

    }
}
