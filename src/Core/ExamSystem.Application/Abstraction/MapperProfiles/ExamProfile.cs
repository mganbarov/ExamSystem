using AutoMapper;
using ExamSystem.Application.DTOs.Exam;
using ExamSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Abstraction.MapperProfiles
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<Exam, GetExamDTO>()
            .ForMember(dest => dest.StudentNumber, opt => opt.MapFrom(src => src.Student.OrderNumber))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName))
            .ForMember(dest => dest.StudentSurname, opt => opt.MapFrom(src => src.Student.Surname))
            .ForMember(dest => dest.LessonCode, opt => opt.MapFrom(src => src.Lesson.Code))
            .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.Lesson.Name));

            CreateMap<Exam, CreateExamDTO>()
                .ForMember(dest => dest.StudentNumber, opt => opt.MapFrom(src => src.Student.OrderNumber))
                .ForMember(dest => dest.LessonCode, opt=>opt.MapFrom(src=>src.Lesson.Code)).ReverseMap();
            CreateMap<Exam, UpdateExamDTO>()
                 .ForMember(dest => dest.StudentNumber, opt => opt.MapFrom(src => src.Student.OrderNumber))
                .ForMember(dest => dest.LessonCode, opt => opt.MapFrom(src => src.Lesson.Code)).ReverseMap();
       
        }
    }
}
