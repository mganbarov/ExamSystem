using ExamSystem.Application.Abstraction.Repositories;
using ExamSystem.Domain;
using ExamSystem.Persistence.Context;
using ExamSystem.Persistence.Implementations.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Persistence.Implementations.Repository
{
    public class LessonRepository : Repository<Lesson>,ILessonRepository
    {
        private readonly ApplicationDbContext _context;
        public LessonRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Lesson?> GetByCodeAsync(string code)
        {
            return await _context.Lessons
                .FirstOrDefaultAsync(l=>l.Code == code);
        }

        public async Task<ICollection<Lesson>> GetByGradeAsync(int grade)
        {
            return await _context.Lessons
                .Where(l => l.Grade == grade)
                .OrderBy(l => l.Name)
                .ToListAsync();
        }

        public IQueryable<Lesson> GetAllWithFilter(int skip, int take, int? grade = null)
        {
            var query = _context.Lessons.AsQueryable();

            if (grade.HasValue)
                query = query.Where(l => l.Grade == grade.Value);

            return query.OrderBy(l => l.Grade)
                       .ThenBy(l => l.Name)
                       .Skip(skip)
                       .Take(take);
        }

        public async Task<bool> HasExamsAsync(int lessonId)
        {
            return await _context.Exams
                .AnyAsync(e => e.LessonId == lessonId);
        }
    }
}
