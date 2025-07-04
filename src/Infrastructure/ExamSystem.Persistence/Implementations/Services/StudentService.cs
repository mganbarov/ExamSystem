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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateStudentDto dto)
        {

            var existingStudent = await _repo.GetByNumberAsync(dto.OrderNumber);
            if (existingStudent != null)
                throw new ValidationException($"Nömrəli {dto.OrderNumber} olan şagird artıq mövcuddur");
            var student = new Student
            {
                OrderNumber = dto.OrderNumber,
                FirstName = dto.FirstName.Trim(),
                Surname = dto.Surname.Trim(),
                Grade = dto.Grade
            };

            await _repo.AddAsync(student);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _repo.GetByIdAsync(id, false)
                ?? throw new NotFoundException("Şagird tapılmadı");

            _repo.Delete(student);
            await _repo.SaveChangesAsync();
        }

        public async Task<ICollection<GetStudentDto>> GetAllAsync(int page, int take, int? grade = null)
        {
            int skip = (page - 1) * take;

            var students = await _repo.GetAllWithFilter(skip, take, grade).ToListAsync();
            var dtos = _mapper.Map<ICollection<GetStudentDto>>(students);

            return dtos;
        }

        public async Task<GetStudentDto> GetByIdAsync(int id)
        {
            var student = await _repo.GetByIdAsync(id)
                ?? throw new NotFoundException("Şagird tapılmadı");

            var dto = _mapper.Map<GetStudentDto>(student);
            return dto;
        }

        public async Task<GetStudentDto> GetByNumberAsync(int number)
        {
            var student = await _repo.GetByNumberAsync(number)
                ?? throw new NotFoundException($"Nömrəsi {number} olan şagird tapılmadı");

            var dto = _mapper.Map<GetStudentDto>(student);
            return dto;
        }

        public async Task UpdateAsync(UpdateStudentDto dto)
        {
            var student = await _repo.GetByIdAsync(dto.Id, true)
                ?? throw new NotFoundException("Şagird tapılmadı");

            var existingStudent = await _repo.GetByNumberAsync(dto.OrderNumber);
            if (existingStudent != null && existingStudent.Id != dto.Id)
                throw new ValidationException($"Nömrəsi {dto.OrderNumber} olan şagird artıq mövcuddur");


            student.OrderNumber = dto.OrderNumber;
            student.FirstName = dto.FirstName.Trim();
            student.Surname = dto.Surname.Trim();
            student.Grade = dto.Grade;

            await _repo.SaveChangesAsync();
        }

        public async Task<ICollection<GetStudentDto>> GetStudentsByGradeAsync(int grade)
        {
            if (grade < 1 || grade > 12)
                throw new ValidationException("Sinif 1-12 aralığında olmalıdır");

            var students = await _repo.GetByGradeAsync(grade);
            var dtos = _mapper.Map<ICollection<GetStudentDto>>(students);

            return dtos;
        }

    }
}
