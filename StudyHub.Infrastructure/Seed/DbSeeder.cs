using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;


namespace StudyHub.Infrastructure.Seed
{
    public class DbSeeder
    {
        public static async Task SeedAsync(StudyHubDbContext context)
        {
            if (context.Users.Any())
                return;

            var teacher = new User
            {
                Id = Guid.NewGuid(),
                UserName = "teacher1",
                Email = "teacher@test.com"
            };

            var student = new User
            {
                Id = Guid.NewGuid(),
                UserName = "student1",
                Email = "student@test.com"
            };

            context.Users.AddRange(teacher, student);

            await context.SaveChangesAsync();
        }
    }
}
