using Microsoft.AspNetCore.Mvc;
using StudyHub.Application.DTOs.Modules;
using StudyHub.Application.Services;

namespace StudyHub.Controllers
{
    [ApiController]
    [Route("api/modules")]
    public class ModuleController : ControllerBase
    {
        private readonly ModuleService _service;

        public ModuleController(ModuleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateModuleDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(id);
        }
    }
}
