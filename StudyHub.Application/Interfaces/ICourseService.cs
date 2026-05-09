using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyHub.Application.DTOs.Courses;

namespace StudyHub.Application.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();

        Task<CourseDto?> GetByIdAsync(Guid id);

        Task<CourseDto> CreateAsync(CreateCourseDto dto);

        Task UpdateAsync(Guid id, UpdateCourseDto dto);

        Task DeleteAsync(Guid id);

        Task PublishAsync(Guid id);

        Task ArchiveAsync(Guid id);

        Task EnrollAsync(Guid courseId, Guid userId);
    }
}
