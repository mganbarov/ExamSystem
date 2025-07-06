using ExamSystem.Domain;


namespace ExamSystem.Application.Abstraction.Services
{
    public interface IStudentService
    {
        Task<ICollection<GetStudentDto>> GetAllAsync(int page, int take, int? grade = null);
        Task<GetStudentDto> GetByIdAsync(int id);
        Task CreateAsync(CreateStudentDto dto);
        Task<GetStudentDto> UpdateAsync(UpdateStudentDto dto);
        Task DeleteAsync(int id);
        
    }
}
