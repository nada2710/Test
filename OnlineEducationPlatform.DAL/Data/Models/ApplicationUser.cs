using Microsoft.AspNetCore.Identity;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineEducationPlatform.DAL.Data.Models
{
   
    public class ApplicationUser : IdentityUser
    {
        public TypeUser UserType { get; set; }
        
        public DateTime CreatedDate { get; set; }


    }
    public class Student : ApplicationUser
    {
        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public ICollection<AnswerResult> AnswerResults { get; set; } = new HashSet<AnswerResult>();

    }
    public class Instructor : ApplicationUser
    {
      
       public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

    }
    public class Admin : ApplicationUser { }
}
public enum TypeUser
{
    Student=1,
    Instructor=2,
    Admin=3
}
