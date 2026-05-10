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
}