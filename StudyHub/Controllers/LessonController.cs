using Microsoft.AspNetCore.Mvc;
using StudyHub.Application.DTOs.Lessons;
using StudyHub.Application.Services;

namespace StudyHub.Controllers
{
    [ApiController]
    [Route("api/lessons")]
    public class LessonController : ControllerBase
    {
        private readonly LessonService _service;

        public LessonController(LessonService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(id);
        }
    }
}
