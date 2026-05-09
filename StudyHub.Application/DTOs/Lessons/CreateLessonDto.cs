using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Lessons
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = null!;
        public Guid ModuleId { get; set; }
    }
}
