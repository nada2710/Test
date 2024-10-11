using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Data.DbHelper
{
    public class EducationPlatformContext : IdentityDbContext<ApplicationUser>
    {
        public EducationPlatformContext(DbContextOptions<EducationPlatformContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<AnswerResult> AnswerResult { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamResult> ExamResult { get; set; }
        public DbSet<Lecture> Lecture { get; set; }
        public DbSet<PdfFile> PdfFile { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<StudentProgress> StudentProgress { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<QuizResult> QuizResult { get; set; }

    }
}
