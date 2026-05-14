using Microsoft.EntityFrameworkCore;
using StudyHub.Application.DTOs.Modules;
using StudyHub.Application.Interfaces;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

public class ModuleService
{
    private readonly StudyHubDbContext _context;
    private readonly INotificationService _notifications;

    public ModuleService(
        StudyHubDbContext context,
        INotificationService notifications)
    {
        _context = context;
        _notifications = notifications;
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
    public async Task UpdateAsync(Guid id, CreateModuleDto dto)
    {
        var module = await _context.Modules.FindAsync(id);

        if (module == null)
            throw new Exception("Module not found");

        module.Title = dto.Title;

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var module = await _context.Modules.FindAsync(id);

        if (module == null)
            throw new Exception("Module not found");

        _context.Modules.Remove(module);

        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Module>> GetByCourseAsync(Guid courseId)
    {
        return await _context.Modules
            .Where(m => m.CourseId == courseId)
            .OrderBy(m => m.Order)
            .ToListAsync();
    }
}