using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please, enter your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
