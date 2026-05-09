using Microsoft.AspNetCore.Mvc;
using StudyHub.Application.DTOs.Courses;
using StudyHub.Application.Interfaces;

namespace StudyHub.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {

        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateCourseDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("{id}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            await _service.PublishAsync(id);

            return Ok();
        }

        [HttpPost("{id}/archive")]
        public async Task<IActionResult> Archive(Guid id)
        {
            await _service.ArchiveAsync(id);

            return Ok();
        }

        [HttpPost("{id}/enroll/{userId}")]
        public async Task<IActionResult> Enroll(
            Guid id,
            Guid userId)
        {
            await _service.EnrollAsync(id, userId);

            return Ok();
        }
    }
}
