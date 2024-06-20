using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TextQuestion> TextQuestions { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<RangeQuestion> RangeQuestions { get; set; }
        public DbSet<DegreeQuestion> DegreeQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure that deleting a Course does not cascade to Questionnaires
            modelBuilder.Entity<Questionnaire>()
                .HasOne(q => q.Course)
                .WithMany(c => c.Questionnaires)
                .HasForeignKey(q => q.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure that deleting a User does not cascade to Answers
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Student)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure that deleting a User does not cascade to Courses
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure that deleting a User does not cascade to Questionnaires
            modelBuilder.Entity<Questionnaire>()
                .HasOne(q => q.Professor)
                .WithMany(u => u.Questionnaires)
                .HasForeignKey(q => q.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
