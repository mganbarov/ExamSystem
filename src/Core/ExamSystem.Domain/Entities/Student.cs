namespace ExamSystem.Domain
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int OrderNumber { get; set; }
        public int Grade { get; set; }
        //Relations
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
