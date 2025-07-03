using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Routes
{
    public struct ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public struct Student
        {
            public const string Create = "students";
            public const string Update = "students";
            public const string GetAll = "students";
            public const string GetById = "students/{id}";
            public const string Delete = "students/{id}";
            
        }

        public struct Exam
        {
            public const string Create = "exams";
            public const string Update = "exams";
            public const string GetAll = "exams";
            public const string GetById = "exams/{id}";
            public const string Delete = "exams/{id}";
        }

        public struct Lesson
        {
            public const string Create = "lessons";
            public const string Update = "lessons";
            public const string GetAll = "lessons";
            public const string GetById = "lessons/{id}";
            public const string Delete = "lessons/{id}";
        }


    }
}
