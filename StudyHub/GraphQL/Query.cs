using Microsoft.EntityFrameworkCore;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

namespace StudyHub.GraphQL
{
    public class Query
    {
        
        public async Task<List<Course>> GetCourses(
            [Service] StudyHubDbContext context)
        {
            return await context.Courses.ToListAsync();
        }

        public async Task<List<User>> GetUsers(
            [Service] StudyHubDbContext context)
        {
            return await context.Users.ToListAsync();
        }

        public async Task<List<Assignment>> GetAssignments(
            [Service] StudyHubDbContext context)
        {
            return await context.Assignments.ToListAsync();
        }
    }
}
