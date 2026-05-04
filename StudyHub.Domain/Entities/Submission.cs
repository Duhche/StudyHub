using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Domain.Entities
{
    public class Submission
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public string Status { get; set; } = "Pending";
        public int? Grade { get; set; }

        public Guid AssigmentId { get; set; }
        public Assigment Assigment { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
