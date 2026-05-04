using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int Order { get; set; }

        public Guid ModuleId { get; set; }
        public Module Module { get; set; } = null!;
    }
}
