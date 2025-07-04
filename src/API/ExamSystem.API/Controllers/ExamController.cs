using ExamSystem.Application;
using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Application.DTOs.Exam;
using ExamSystem.Application.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route(ApiRoutes.Base)]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet(ApiRoutes.Exam.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        => Ok(await _examService.GetAllAsync(page, pageSize));

        [HttpGet(ApiRoutes.Exam.GetById)]
        public async Task<IActionResult> Get(int id) => Ok(await _examService.GetByIdAsync(id));

        [HttpPost(ApiRoutes.Exam.Create)]
        [Authorize]
        public async Task<IActionResult> Create(CreateExamDTO dto)
        {
            await _examService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateExamDTO dto)
        {
            await _examService.UpdateAsync(dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete(ApiRoutes.Exam.Delete)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _examService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}
