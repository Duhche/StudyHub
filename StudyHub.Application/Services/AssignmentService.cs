using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyHub.Application.DTOs.Assigments;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

namespace StudyHub.Application.Services
{
    public class AssignmentService
    {
        private readonly StudyHubDbContext _context;

        public AssignmentService(StudyHubDbContext context)
        {
            _context = context;
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

            return assignment.Id;
        }
    }
}
