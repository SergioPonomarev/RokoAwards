using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please, enter the password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&]).*$", ErrorMessage = "Password must contain at least one uppercase, lowercase, numeric and special character")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 and at max 100 characters long", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password is not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
