
using FluentValidation;

namespace ExamSystem.Application.Validators.Exam
{
    public class UpdateExamValidator : AbstractValidator<UpdateExamDTO>
    {
        public UpdateExamValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => x > 0).WithMessage("Id 0 - dan böyük olmalıdır!");

            RuleFor(x => x.LessonId)
           .NotEmpty()
           .Must(x => x > 0).WithMessage("Id 0-dan böyük olmalıdır!");


            RuleFor(x => x.StudentId)
                .NotEmpty()
                .Must(x => x > 0).WithMessage("Id 0-dan böyük olmalıdır!");

            RuleFor(x => x.ExamDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today);

            RuleFor(x => x.ExamScore)
                .InclusiveBetween(0, 5);
        }

    }
}
