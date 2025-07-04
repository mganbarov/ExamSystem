using AutoMapper;
using ExamSystem.Domain;

namespace ExamSystem.Application.Abstraction.MapperProfiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, GetStudentDto>().ReverseMap();
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            CreateMap<Student, UpdateStudentDto>().ReverseMap();
        }
    }
}
