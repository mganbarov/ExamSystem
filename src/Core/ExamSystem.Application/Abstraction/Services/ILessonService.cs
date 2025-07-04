namespace ExamSystem.Application.Abstraction.Services
{
    public interface ILessonService
    {
        Task<ICollection<GetLessonDto>> GetAllAsync(int page, int take, int? grade = null);
        Task<GetLessonDto> GetByIdAsync(int id);
        Task CreateAsync(CreateLessonDto dto);
        Task UpdateAsync(UpdateLessonDto dto);
        Task DeleteAsync(int id);

    }
}
