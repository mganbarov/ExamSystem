using AutoMapper;
using ExamSystem.Application;
using ExamSystem.Application.Abstraction.Repositories;
using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Application.Exceptions.Common;
using ExamSystem.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence.Implementations.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _repo;
        private readonly IMapper _mapper;

        public LessonService(ILessonRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateLessonDto dto)
        {

            var existingLesson = await _repo.GetByCodeAsync(dto.Code.ToUpper());
            if (existingLesson != null)
                throw new ValidationException($"Kodu {dto.Code} olan dərs artıq mövcuddur");

            var lesson = new Lesson
            {
                Code = dto.Code.ToUpper(), 
                Name = dto.Name.Trim(),
                Grade = dto.Grade,
                TeacherFirstName = dto.TeacherFirstName.Trim(),
                TeacherSurname = dto.TeacherSurname.Trim()
            };

            await _repo.AddAsync(lesson);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson = await _repo.GetByIdAsync(id, false)
                ?? throw new NotFoundException("Dərs tapılmadı");

            
            var hasExams = await _repo.HasExamsAsync(id);
            if (hasExams)
                throw new ValidationException("Bu dərsə aid imtahanlar mövcuddur. Əvvəlcə imtahanları silin");

            _repo.Delete(lesson);
            await _repo.SaveChangesAsync();
        }

        public async Task<ICollection<GetLessonDto>> GetAllAsync(int page, int take, int? grade = null)
        {
            int skip = (page - 1) * take;

            var lessons = await _repo.GetAllWithFilter(skip, take, grade).ToListAsync();
            var dtos = _mapper.Map<ICollection<GetLessonDto>>(lessons);

            return dtos;
        }

        public async Task<GetLessonDto> GetByIdAsync(int id)
        {
            var lesson = await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException("Dərs tapılmadı");

            var dto = _mapper.Map<GetLessonDto>(lesson);
            return dto;
        }

        public async Task<GetLessonDto> GetByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ValidationException("Dərs kodu boş ola bilməz");

            var lesson = await _repo.GetByCodeAsync(code.ToUpper())
                ?? throw new NotFoundException($"Kodu {code} olan dərs tapılmadı");

            var dto = _mapper.Map<GetLessonDto>(lesson);
            return dto;
        }

        public async Task UpdateAsync(UpdateLessonDto dto)
        {
            var lesson = await _repo.GetByIdAsync(dto.Id, true)
                ?? throw new NotFoundException("Dərs tapılmadı");



            if (lesson.Code != dto.Code.ToUpper())
            {
                var existingLesson = await _repo.GetByCodeAsync(dto.Code);
                if (existingLesson != null)
                    throw new ValidationException($"Kodu {dto.Code} olan dərs artıq mövcuddur");
            }

            
            lesson.Code = dto.Code.ToUpper();
            lesson.Name = dto.Name.Trim();
            lesson.Grade = dto.Grade;
            lesson.TeacherFirstName = dto.TeacherFirstName.Trim();
            lesson.TeacherSurname = dto.TeacherSurname.Trim();

            await _repo.SaveChangesAsync();
        }

        public async Task<ICollection<GetLessonDto>> GetLessonsByGradeAsync(int grade)
        {
            if (grade < 1 || grade > 12)
                throw new ValidationException("Sinif 1-12 aralığında olmalıdır");

            var lessons = await _repo.GetByGradeAsync(grade);
            var dtos = _mapper.Map<ICollection<GetLessonDto>>(lessons);

            return dtos;
        }
  
    }
}
