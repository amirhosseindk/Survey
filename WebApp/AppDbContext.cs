using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TextQuestion> TextQuestions { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<RangeQuestion> RangeQuestions { get; set; }
        public DbSet<DegreeQuestion> DegreeQuestions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<MultipleChoiceOption> MultipleChoiceOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User and Courses (Professors teaching courses)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(u => u.TaughtCourses)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course and Classes
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Classes)
                .WithOne(cl => cl.Course)
                .HasForeignKey(cl => cl.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Class and Students (Many-to-Many)
            modelBuilder.Entity<Class>()
                .HasMany(cl => cl.Students)
                .WithMany(u => u.Classes)
                .UsingEntity(j => j.ToTable("ClassStudents"));

            // Class and Questionnaires
            modelBuilder.Entity<Class>()
                .HasMany(cl => cl.Questionnaires)
                .WithOne(q => q.Class)
                .HasForeignKey(q => q.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // Questionnaire and Professor
            modelBuilder.Entity<Questionnaire>()
                .HasOne(q => q.Professor)
                .WithMany()
                .HasForeignKey(q => q.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Questionnaire and Questions
            modelBuilder.Entity<Questionnaire>()
                .HasMany(qn => qn.Questions)
                .WithOne(q => q.Questionnaire)
                .HasForeignKey(q => q.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question inheritance (Table-per-Hierarchy)
            modelBuilder.Entity<Question>()
                .HasDiscriminator(q => q.Type)
                .HasValue<TextQuestion>(QuestionType.Text)
                .HasValue<MultipleChoiceQuestion>(QuestionType.MultipleChoice)
                .HasValue<RangeQuestion>(QuestionType.Range)
                .HasValue<DegreeQuestion>(QuestionType.Degree);

            // MultipleChoiceQuestion and Options
            modelBuilder.Entity<MultipleChoiceQuestion>()
                .HasMany(mq => mq.Options)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Answer and Student
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Student)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer and Questionnaire
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Questionnaire)
                .WithMany(qn => qn.Answers)
                .HasForeignKey(a => a.QuestionnaireId)
                .OnDelete(DeleteBehavior.Cascade);

            // Answer and Question
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer and MultipleChoiceOption
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.AnswerOption)
                .WithMany()
                .HasForeignKey(a => a.AnswerOptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}