using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyHub.Application.DTOs.Modules;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

namespace StudyHub.Application.Services
{
    public class ModuleService
    {
        private readonly StudyHubDbContext _context;

        public ModuleService(StudyHubDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateModuleDto dto)
        {
            var course = await _context.Courses.FindAsync(dto.CourseId);

            if (course == null)
                throw new Exception("Course not found");

            if (!course.IsPublished)
                throw new Exception("Cannot add module to unpublished course");

            var module = new Module
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                CourseId = dto.CourseId
            };

            _context.Modules.Add(module);
            await _context.SaveChangesAsync();

            return module.Id;
        }
    }
}
