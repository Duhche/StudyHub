using Microsoft.AspNetCore.SignalR;
using StudyHub.Hubs;
using StudyHub.Application.Interfaces;

namespace StudyHub
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hub;

        public SignalRNotificationService(
            IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        public async Task CoursePublished(Guid courseId, string title)
        {
            await _hub.Clients.All.SendAsync(
                "CoursePublished",
                new
                {
                    courseId,
                    title
                });
        }

        public async Task UserEnrolled(Guid userId, Guid courseId)
        {
            await _hub.Clients.All.SendAsync(
                "UserEnrolled",
                new
                {
                    userId,
                    courseId
                });
        }

        public async Task AssignmentCreated(Guid assignmentId, string title)
        {
            await _hub.Clients.All.SendAsync(
                "AssignmentCreated",
                new
                {
                    assignmentId,
                    title
                });
        }
    }
}
