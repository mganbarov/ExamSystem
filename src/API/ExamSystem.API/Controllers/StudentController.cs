using ExamSystem.Application;
using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Application.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route(ApiRoutes.Base)]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet(ApiRoutes.Student.GetAll)]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
            => Ok(await _studentService.GetAllAsync(page, pageSize));

        [HttpGet(ApiRoutes.Student.GetById)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _studentService.GetByIdAsync(id));

        [HttpPost(ApiRoutes.Student.Create)]
        [Authorize]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            await _studentService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut(ApiRoutes.Student.Update)]
        [Authorize]
        public async Task<IActionResult> Update(UpdateStudentDto dto)
        {
            var response = await _studentService.UpdateAsync(dto);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.Student.Delete)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
