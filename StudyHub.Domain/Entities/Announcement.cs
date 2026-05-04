using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Entities
{
    public class Announcement
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
