using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyHub.Application.DTOs.Lessons;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

namespace StudyHub.Application.Services
{
    public class LessonService
    {
        private readonly StudyHubDbContext _context;

        public LessonService(StudyHubDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateLessonDto dto)
        {
            var module = await _context.Modules.FindAsync(dto.ModuleId);

            if (module == null)
                throw new Exception("Module does not exist");

            var lesson = new Lesson
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                ModuleId = dto.ModuleId
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return lesson.Id;
        }
    }
}
