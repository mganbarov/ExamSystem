using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain
{
    public class Exam : BaseEntity
    {
        public DateTime ExamDate { get; set; }
        public int ExamScore { get; set; }
        //Relations
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
