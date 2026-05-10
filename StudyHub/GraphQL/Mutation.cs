using StudyHub.Application.DTOs.Courses;
using StudyHub.Application.Interfaces;
using StudyHub.Application.Services;

namespace StudyHub.GraphQL
{
    public class Mutation
    {

        public async Task<CourseDto> CreateCourse(
     CreateCourseDto dto,
     [Service] ICourseService service)
        {
            return await service.CreateAsync(dto);
        }

        public async Task PublishCourse(
            Guid id,
            [Service] ICourseService service)
        {
            await service.PublishAsync(id);
        }
    }
}
