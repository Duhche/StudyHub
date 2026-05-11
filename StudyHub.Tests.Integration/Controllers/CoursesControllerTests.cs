using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace StudyHub.Tests.Integration.Controllers
{
    public class CoursesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CoursesControllerTests(
            WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCourses_ShouldReturnSuccess()
        {
            // Act
            var response = await _client.GetAsync("/api/courses");

            // Assert
            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task CreateCourse_ShouldReturnSuccess()
        {
            var json = """
    {
        "title": "Integration Course"
    }
    """;

            var content = new StringContent(
                json,
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(
                "/api/courses",
                content);

            response.StatusCode
                .Should()
                .Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task CreateCourse_WithInvalidData_ShouldFail()
        {
            var json = """
    {
        "title": ""
    }
    """;

            var content = new StringContent(
                json,
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(
                "/api/courses",
                content);

            response.StatusCode
                .Should()
                .NotBe(HttpStatusCode.OK);
        }
    }
}
