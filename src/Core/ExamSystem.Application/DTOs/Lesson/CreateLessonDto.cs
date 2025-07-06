using System.Text.Json.Serialization;

namespace ExamSystem.Application
{
    public record CreateLessonDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = null!;
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("grade")]
        public int Grade { get; set; }
        [JsonPropertyName("teacherFirstName")]
        public string TeacherFirstName { get; set; } = null!;
        [JsonPropertyName("teacherSurname")]
        public string TeacherSurname { get; set; } = null!;
    }
}
