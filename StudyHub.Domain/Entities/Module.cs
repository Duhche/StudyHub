using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Entities
{
    public class Module
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int Order { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
