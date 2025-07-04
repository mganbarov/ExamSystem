using AutoMapper;
using ExamSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Abstraction.MapperProfiles
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, GetLessonDto>().ReverseMap();
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();
            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();
        }
    }
}
