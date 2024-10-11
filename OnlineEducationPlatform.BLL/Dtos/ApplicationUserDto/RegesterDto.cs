using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.Dtos.ApplicationUserDto
{
    public class RegesterDto
    {
        [Required, StringLength(50)]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
        public TypeUser UserType { get; set; }





    }
}
