using System;

namespace StudyHub.Domain.Entities;

public class Enrollment
{
   
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Guid CourseId { get; set; }

    public Course Course { get; set; } = null!;

    public DateTime EnrolledAt { get; set; }
}