using ExamSystem.Application.Abstraction.Repositories;
using FluentValidation;

namespace ExamSystem.Application
{
    public class CreateLessonValidator : AbstractValidator<CreateLessonDto>
    {
        public CreateLessonValidator() 
        {

            RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Dərs kodu boş ola bilməz")
            .Length(3)
            .WithMessage("Dərs kodu 3 simvoldan ibarət olmalıdır");
           
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Dərs adı boş ola bilməz")
                .MaximumLength(30)
                .WithMessage("Dərs adı 30 simvoldan çox ola bilməz");

            RuleFor(x => x.Grade)
                .InclusiveBetween(1, 12)
                .WithMessage("Sinif 1-12 aralığında olmalıdır");

            RuleFor(x => x.TeacherFirstName)
                .NotEmpty()
                .WithMessage("Müəllimin adı boş ola bilməz")
                .MaximumLength(20)
                .WithMessage("Müəllimin adı 20 simvoldan çox ola bilməz");

            RuleFor(x => x.TeacherSurname)
                .NotEmpty()
                .WithMessage("Müəllimin soyadı boş ola bilməz")
                .MaximumLength(20)
                .WithMessage("Müəllimin soyadı 20 simvoldan çox ola bilməz");
        }
        
    }
}
