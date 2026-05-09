using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.DTOs.Assigments
{
    public class CreateAssignmentDto
    {
        public string Title { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public Guid CourseId { get; set; }
    }
}
