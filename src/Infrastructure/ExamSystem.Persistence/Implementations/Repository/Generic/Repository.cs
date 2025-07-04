using ExamSystem.Application.Abstraction.Repositories.Generic;
using ExamSystem.Domain;
using ExamSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence.Implementations.Repository.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly DbSet<T> _table;
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<T> GetAll(int? skip = null, int? take = null, bool isTracking = false, bool ignoreQuery = false, Expression<Func<T, object>>? expressionIncludes = null, params string[] includes)
        {
            IQueryable<T> query = _table;
            if (!isTracking)
                query = query.AsNoTracking();

            if (expressionIncludes != null)
                query = query.Include(expressionIncludes);

            query = addIncludes(query, includes);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }
        public async Task<T?> GetByIdAsync(int id, bool isTracking = true, bool ignoreQuery = false, Expression<Func<T,object>>? expressionIncludes = null,params string[] includes)
        {
            IQueryable<T> query = _table;
            if (ignoreQuery) query = query.IgnoreQueryFilters();
            query = query.Where(e => e.Id == id);
            if(expressionIncludes is not null)
            {
                query = query.Include(expressionIncludes);
            }
            if(includes != null)
            {
                query = addIncludes(query, includes);
            }
            return isTracking ? await query.FirstOrDefaultAsync() : await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }
       
        private IQueryable<T> addIncludes(IQueryable<T> query, params string[] includes)
        {
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }

    }
}
