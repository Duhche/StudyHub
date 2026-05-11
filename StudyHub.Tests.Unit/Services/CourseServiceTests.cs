using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using StudyHub.Application.DTOs.Courses;
using StudyHub.Application.Interfaces;
using StudyHub.Application.Services;
using StudyHub.Domain.Entities;
using StudyHub.Infrastructure.Data;

namespace StudyHub.Tests.Unit.Services
{
    public class CourseServiceTests
    {
        private StudyHubDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<StudyHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StudyHubDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateCourse()
        {
            // Arrange
            var context = GetDbContext();

            var notifications = new Mock<INotificationService>();

            var service = new CourseService(
                context,
                notifications.Object);

            var dto = new CreateCourseDto
            {
                Title = "Test Course"
            };

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();

            context.Courses.Count()
                .Should()
                .Be(1);
        }
        [Fact]
        public async Task EnrollAsync_ShouldThrow_WhenCourseNotPublished()
        {
            var context = GetDbContext();

            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Hidden",
                IsPublished = false
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "student",
                Email = "student@test.com"
            };

            context.Courses.Add(course);
            context.Users.Add(user);

            await context.SaveChangesAsync();

            var notifications = new Mock<INotificationService>();

            var service = new CourseService(
                context,
                notifications.Object);

            Func<Task> action = async () =>
                await service.EnrollAsync(course.Id, user.Id);

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage("Cannot enroll in unpublished course");
        }
        [Fact]
        public async Task EnrollAsync_ShouldThrow_WhenAlreadyEnrolled()
        {
            var context = GetDbContext();

            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Published",
                IsPublished = true
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "student",
                Email = "student@test.com"
            };

            context.Courses.Add(course);
            context.Users.Add(user);

            context.Enrollments.Add(new Enrollment
            {
                Id = Guid.NewGuid(),
                CourseId = course.Id,
                UserId = user.Id,
                EnrolledAt = DateTime.UtcNow
            });

            await context.SaveChangesAsync();

            var notifications = new Mock<INotificationService>();

            var service = new CourseService(
                context,
                notifications.Object);

            Func<Task> action = async () =>
                await service.EnrollAsync(course.Id, user.Id);

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage("User already enrolled");
        }
    }
}
