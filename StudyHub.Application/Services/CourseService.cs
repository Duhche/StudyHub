using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyHub.Application.DTOs.Courses;
using StudyHub.Application.Interfaces;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;


namespace StudyHub.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly StudyHubDbContext _context;

        private readonly INotificationService _notifications;

        public CourseService(
    StudyHubDbContext context,
    INotificationService notifications)
        {
            _context = context;
            _notifications = notifications;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    IsPublished = c.IsPublished
                })
                .ToListAsync();
        }

        public async Task<CourseDto?> GetByIdAsync(Guid id)
        {
            return await _context.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    IsPublished = c.IsPublished
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                IsPublished = false
            };

            _context.Courses.Add(course);

            await _context.SaveChangesAsync();

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                IsPublished = course.IsPublished
            };
        }

        public async Task UpdateAsync(Guid id, UpdateCourseDto dto)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                throw new Exception("Course not found");

            course.Title = dto.Title;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var course = await _context.Courses
                .Include(c => c.Modules)
                .Include(c => c.Assigments)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                throw new Exception("Course not found");

            if (course.Modules.Any() ||
                course.Assigments.Any() ||
                course.Enrollments.Any())
            {
                throw new Exception(
                    "Cannot delete course with related data");
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();
        }

        public async Task PublishAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                throw new Exception("Course not found");

            course.IsPublished = true;

            await _context.SaveChangesAsync();

            await _notifications.CoursePublished(
       course.Id,
       course.Title);
        }

        public async Task ArchiveAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                throw new Exception("Course not found");

            course.IsPublished = false;

            await _context.SaveChangesAsync();
        }

        public async Task EnrollAsync(Guid courseId, Guid userId)
        {
            var course = await _context.Courses.FindAsync(courseId);

            if (course == null)
                throw new Exception("Course not found");

            if (!course.IsPublished)
            {
                throw new Exception(
                    "Cannot enroll in unpublished course");
            }

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            bool alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e =>
                    e.CourseId == courseId &&
                    e.UserId == userId);

            if (alreadyEnrolled)
            {
                throw new Exception(
                    "User already enrolled");
            }

            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                CourseId = courseId,
                UserId = userId,
                EnrolledAt = DateTime.UtcNow
            };

            _context.Enrollments.Add(enrollment);

            await _context.SaveChangesAsync();

            await _notifications.UserEnrolled(
    userId,
    courseId);
        }
    }
}
