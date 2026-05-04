using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public String Title { get; set; } = null!;
        public bool IsPublished { get; set; }

        public ICollection<Module> Modules { get; set; } = new List<Module>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Assigment> Assigments { get; set; } = new List<Assigment>();
        public ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}
