using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Application.Interfaces
{
    public interface INotificationService
    {
        Task CoursePublished(Guid courseId, string title);

        Task UserEnrolled(Guid userId, Guid courseId);

        Task AssignmentCreated(Guid assignmentId, string title);


    }
}
