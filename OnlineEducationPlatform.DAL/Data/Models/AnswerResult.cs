using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineEducationPlatform.DAL.Data.Models
{
    public class AnswerResult
    {
        public int Id { get; set; }
        public string StudentAnswer { get; set; }
        public decimal MarksAwarded { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public Student student { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("Answer")]
        public int AnswerId { get; set; }
       
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}