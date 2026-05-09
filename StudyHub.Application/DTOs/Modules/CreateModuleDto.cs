using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Modules
{
    public class CreateModuleDto
    {
        public string Title { get; set; } = null!;
        public Guid CourseId { get; set; }
    }
}
