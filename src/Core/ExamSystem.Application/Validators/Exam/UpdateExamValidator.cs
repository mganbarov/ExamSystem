
using ExamSystem.Application.Abstraction.Repositories;
using ExamSystem.Application.DTOs.Exam;
using FluentValidation;

namespace ExamSystem.Application.Validators.Exam
{
    public class UpdateExamValidator : AbstractValidator<UpdateExamDTO>
    {
        public UpdateExamValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id 0-dan böyük olmalıdır!");

            RuleFor(x => x.StudentNumber)
            .NotEmpty()
            .WithMessage("Şagird nömrəsi boş ola bilməz")
            .WithMessage("Bu nömrəli şagird tapılmadı");

            RuleFor(x => x.LessonCode)
                .NotEmpty()
                .WithMessage("Dərs kodu boş ola bilməz");

            RuleFor(x => x.ExamDate)
                .NotEmpty()
                .WithMessage("İmtahan tarixi boş ola bilməz")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("İmtahan tarixi gələcəkdə ola bilməz");

            RuleFor(x => x.ExamScore)
                .InclusiveBetween(0, 9)
                .WithMessage("Qiymət 0-9 aralığında olmalıdır");

        }

    }
}
