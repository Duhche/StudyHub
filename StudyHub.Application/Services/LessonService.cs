using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyHub.Application.DTOs.Lessons;
using StudyHub.Application.Interfaces;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

namespace StudyHub.Application.Services
{
    public class LessonService
    {
        private readonly StudyHubDbContext _context;

        private readonly INotificationService _notifications;

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
        public async Task UpdateAsync(Guid id, CreateLessonDto dto)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
                throw new Exception("Lesson not found");

            lesson.Title = dto.Title;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
                throw new Exception("Lesson not found");

            _context.Lessons.Remove(lesson);

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Lesson>> GetByModuleAsync(Guid moduleId)
        {
            return await _context.Lessons
                .Where(l => l.ModuleId == moduleId)
                .OrderBy(l => l.Order)
                .ToListAsync();
        }
    }
}
