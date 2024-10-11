using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public decimal Score { get; set; }
        public decimal TotalMarks { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        [ForeignKey("StudentProgress")]
        public int StudentProgressId { get; set; }
        public StudentProgress StudentProgress { get; set; }
        public Quiz Quiz { get; set; }
    }
}