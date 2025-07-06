using ExamSystem.Application;
using ExamSystem.Application.Abstraction.Services;
using ExamSystem.Application.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route(ApiRoutes.Base)]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet(ApiRoutes.Lesson.GetAll)]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
         => Ok(await _lessonService.GetAllAsync(page, pageSize));

        [HttpGet(ApiRoutes.Lesson.GetById)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _lessonService.GetByIdAsync(id));

        [HttpPost(ApiRoutes.Lesson.Create)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]CreateLessonDto dto)
        {
            await _lessonService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut(ApiRoutes.Lesson.Update)]
        [Authorize]
        public async Task<IActionResult> Update(UpdateLessonDto dto)
        {
            await _lessonService.UpdateAsync(dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete(ApiRoutes.Lesson.Delete)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
