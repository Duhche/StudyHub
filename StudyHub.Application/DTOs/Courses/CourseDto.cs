using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Courses
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public bool IsPublished { get; set; }
    }
}
