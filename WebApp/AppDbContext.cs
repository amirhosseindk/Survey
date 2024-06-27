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
        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(u => u.TaughtCourses)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Classes)
                .WithOne(cl => cl.Course)
                .HasForeignKey(cl => cl.CourseId);

            modelBuilder.Entity<Class>()
                .HasMany(cl => cl.Students)
                .WithMany(u => u.Classes)
                .UsingEntity(j => j.ToTable("ClassStudents"));

            modelBuilder.Entity<Class>()
                .HasMany(cl => cl.Questionnaires)
                .WithOne(q => q.Class)
                .HasForeignKey(q => q.ClassId);

            modelBuilder.Entity<Questionnaire>()
                .HasOne(q => q.Professor)
                .WithMany(u => u.Questionnaires)
                .HasForeignKey(q => q.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Student)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}