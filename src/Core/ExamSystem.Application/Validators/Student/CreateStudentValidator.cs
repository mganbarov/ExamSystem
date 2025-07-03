using FluentValidation;

namespace ExamSystem.Application.Validators.Student
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentValidator()
        {
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
