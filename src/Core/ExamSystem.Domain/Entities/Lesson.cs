
namespace ExamSystem.Domain
{
    public class Lesson : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherSurname { get; set; }
        //Relations
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
