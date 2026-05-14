using Microsoft.EntityFrameworkCore;
using StudyHub.Application.DTOs.Assigments;
using StudyHub.Application.Interfaces;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

public class AssignmentService
{
    private readonly StudyHubDbContext _context;
    private readonly INotificationService _notifications;

    public AssignmentService(
        StudyHubDbContext context,
        INotificationService notifications)
    {
        _context = context;
        _notifications = notifications;
    }

    public async Task<Guid> CreateAsync(CreateAssignmentDto dto)
    {
        var course = await _context.Courses.FindAsync(dto.CourseId);

        if (course == null)
            throw new Exception("Course not found");

        if (!course.IsPublished)
            throw new Exception("Cannot add assignment to unpublished course");

        if (dto.Deadline < DateTime.UtcNow)
            throw new Exception("Deadline cannot be in the past");

        var assignment = new Assignment
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            DeadLine = dto.Deadline,
            CourseId = dto.CourseId
        };

        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        
        await _notifications.AssignmentCreated(
            assignment.Id,
            assignment.Title);

        return assignment.Id;
    }
    public async Task UpdateAsync(Guid id, CreateAssignmentDto dto)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
            throw new Exception("Assignment not found");

        if (dto.Deadline < DateTime.UtcNow)
            throw new Exception("Deadline cannot be in the past");

        assignment.Title = dto.Title;
        assignment.DeadLine = dto.Deadline;

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(Guid id)
    {
        var assignment = await _context.Assignments.FindAsync(id);

        if (assignment == null)
            throw new Exception("Assignment not found");

        _context.Assignments.Remove(assignment);

        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Assignment>> GetByCourseAsync(Guid courseId)
    {
        return await _context.Assignments
            .Where(a => a.CourseId == courseId)
            .ToListAsync();
    }
}