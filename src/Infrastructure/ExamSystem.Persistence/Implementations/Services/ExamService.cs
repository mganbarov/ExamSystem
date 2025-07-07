using AutoMapper;
using ExamSystem.Application;
using ExamSystem.Application.Abstraction.Repositories;
using ExamSystem.Application.Abstraction.Repositories.Generic;
using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Application.DTOs.Exam;
using ExamSystem.Application.Exceptions.Common;
using ExamSystem.Domain;
using Microsoft.EntityFrameworkCore;


namespace ExamSystem.Persistence.Implementations.Services
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _repo;
        private readonly IStudentRepository _studentRepo;
        private readonly ILessonRepository _lessonRepo;
        private readonly IMapper _mapper;
        public ExamService(IExamRepository repo, IMapper mapper, IStudentRepository studentRepo, ILessonRepository lessonRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _studentRepo = studentRepo;
            _lessonRepo = lessonRepo;
        }


        public async Task CreateAsync(CreateExamDTO dto)
        {
            var student = await _studentRepo.GetByNumberAsync(dto.StudentNumber);
            if (student == null)
                throw new ValidationException($"Şagird nömrəsi {dto.StudentNumber} ilə tapılmadı");

            var lesson = await _lessonRepo.GetByCodeAsync(dto.LessonCode);
            if (lesson == null)
                throw new ValidationException($"Dərs kodu {dto.LessonCode} ilə tapılmadı");

            if (student.Grade != lesson.Grade)
                throw new ValidationException("Şagirdin sinifi dərsin sinifi ilə uyğun deyil");

            var existingExam = await _repo.GetByStudentAndLessonAsync(student.Id, lesson.Id);
            if (existingExam != null)
                throw new ValidationException("Bu şagird artıq bu dərsdən imtahan verib");

            if (dto.ExamDate > DateTime.Today)
                throw new ValidationException("İmtahan tarixi gələcəkdə ola bilməz");

            var exam = new Exam
            {
                ExamDate = dto.ExamDate.Value,
                ExamScore = dto.ExamScore,
                StudentId = student.Id,
                LessonId = lesson.Id,
            };

            await _repo.AddAsync(exam);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var exam = await _repo.GetByIdAsync(id, false)
                ?? throw new NotFoundException("Məlumat tapılmadı");
            _repo.Delete(exam);
            await _repo.SaveChangesAsync();
        }

        public async Task<(List<GetExamDTO> items, int totalCount)> GetAllAsync(int page, int take)
        {
            int skip = (page - 1) * take;

            var query = _repo.GetAll(0, int.MaxValue, false, true, includes: new[] { nameof(Student), nameof(Lesson) });
            var totalCount = await query.CountAsync();

            var pagedQuery = query.Skip(skip).Take(take);

            var items = await pagedQuery
                .Select(exam => new GetExamDTO(
                    exam.Id,
                    exam.ExamDate,
                    exam.Lesson.Name,
                    exam.Student.FirstName,
                    exam.Student.Surname,
                    exam.ExamScore,
                    exam.Student.OrderNumber,
                    exam.Lesson.Code
                )).ToListAsync();

            return (items, totalCount);

        }

        public async Task<GetExamDTO> GetByIdAsync(int id)
        {
            var exam = await _repo.GetByIdAsync(id, includes: new[] { nameof(Student), nameof(Lesson) }) ?? throw new NotFoundException("Məlumat tapılmadı");
            var dto = _mapper.Map<GetExamDTO>(exam);
            return dto;
        }

        public async Task UpdateAsync(UpdateExamDTO dto)
        {
            var exam = await _repo.GetByIdAsync(dto.Id, true)
        ?? throw new NotFoundException("Məlumat tapılmadı");

            var student = await _studentRepo.GetByNumberAsync(dto.StudentNumber);
            if (student == null)
                throw new ValidationException($"Şagird nömrəsi {dto.StudentNumber} ilə tapılmadı");

            var lesson = await _lessonRepo.GetByCodeAsync(dto.LessonCode);
            if (lesson == null)
                throw new ValidationException($"Dərs kodu {dto.LessonCode} ilə tapılmadı");

            if (dto.ExamDate > DateTime.Today)
                throw new ValidationException("İmtahan tarixi gələcəkdə ola bilməz");

            if (dto.ExamScore < 0 || dto.ExamScore > 9)
                throw new ValidationException("Qiymət 0-9 aralığında olmalıdır");

            if (student.Grade != lesson.Grade)
                throw new ValidationException("Şagirdin sinifi dərsin sinifi ilə uyğun deyil");

            var existingExam = await _repo.GetByStudentAndLessonAsync(student.Id, lesson.Id);
            if (existingExam != null && existingExam.Id != dto.Id)
                throw new ValidationException("Bu şagird artıq bu dərsdən imtahan verib");

            exam.ExamDate = dto.ExamDate;
            exam.ExamScore = dto.ExamScore;
            exam.StudentId = student.Id;
            exam.LessonId = lesson.Id;

            await _repo.SaveChangesAsync();

        }
    }
}
