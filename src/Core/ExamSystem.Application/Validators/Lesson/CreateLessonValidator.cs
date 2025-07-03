using FluentValidation;

namespace ExamSystem.Application
{
    public class CreateLessonValidator : AbstractValidator<CreateLessonDto>
    {
        public CreateLessonValidator() 
        {
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
