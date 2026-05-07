using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyHub.Domain.Entities;
namespace StudyHub.Infrastructure.Data
{
    public class StudyHubDbContext : DbContext
    {
        public StudyHubDbContext(DbContextOptions<StudyHubDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<Submission> Submissions => Set<Submission>();
        public DbSet<Announcement> Announcements => Set<Announcement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.UserId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Module>()
                .HasOne(m => m.Course)
                .WithMany(c => c.Modules)
                .HasForeignKey(m => m.CourseId);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Module)
                .WithMany(m => m.Lessons)
                .HasForeignKey(l => l.ModuleId);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assigments)
                .HasForeignKey(a => a.CourseId);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Assigment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssigmentId);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.CourseId);
        }
    }
}
