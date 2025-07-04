using ExamSystem.Application.Abstraction.Repositories;
using FluentValidation;

namespace ExamSystem.Application.Validators.Student
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidator()
        {
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
