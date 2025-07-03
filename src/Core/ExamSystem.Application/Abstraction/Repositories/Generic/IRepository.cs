using ExamSystem.Domain;
using System.Linq.Expressions;

namespace ExamSystem.Application.Abstraction.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(
            int? skip = null,
            int? take = null,
            bool isTracking = false,
            bool ignoreQuery = false,
            Expression<Func<T, object>>? expressionIncludes = null,
            params string[] includes);
        Task<T> GetByIdAsync(int id, bool isTracking = true, bool ignoreQuery = false, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity);

        void Delete(T entity);

        void SoftDelete(T entity);
        void ReverseSoftDelete(T entity);

        Task SaveChangesAsync();

    }
}
