using ExamSystem.Domain;


namespace ExamSystem.Application.Abstraction.Services
{
    public interface IService<T> where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync(int page, int take);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task Delete(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDelete(int id);

    }
}
