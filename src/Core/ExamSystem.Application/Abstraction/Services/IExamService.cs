using ExamSystem.Application.DTOs.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Abstraction.Services
{
    public interface IExamService
    {
        Task<ICollection<GetExamDTO>> GetAllAsync(int page, int take);
        Task<GetExamDTO> GetByIdAsync(int id);
        Task CreateAsync(CreateExamDTO dto);
        Task UpdateAsync(UpdateExamDTO dto);
        Task DeleteAsync(int id);
    }
}
