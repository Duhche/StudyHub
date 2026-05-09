using Microsoft.AspNetCore.Mvc;
using StudyHub.Application.DTOs.Assigments;
using StudyHub.Application.Services;

namespace StudyHub.Controllers
{
    [ApiController]
    [Route("api/assignments")]
    public class AssignmentController : ControllerBase 
    {
        private readonly AssignmentService _service;

        public AssignmentController(AssignmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssignmentDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(id);
        }
    }
}
