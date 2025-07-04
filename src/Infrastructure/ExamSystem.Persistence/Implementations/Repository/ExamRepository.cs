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
    public class ExamRepository : Repository<Exam>, IExamRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Exam?> GetByStudentAndLessonAsync(int studentId, int lessonId)
        {
            return await _context.Exams
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.LessonId == lessonId);
        }
    }
}
