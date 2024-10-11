using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class StudentProgress
    {
        public int Id { get; set; }
        public decimal OverallProgress { get; set; }
        public DateTime LastUpdated { get; set; }

        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public Enrollment Enrollment { get; set; }
        public ICollection<QuizResult> QuizResults { get; set; } = new HashSet<QuizResult>();
        public ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();




    }
}

