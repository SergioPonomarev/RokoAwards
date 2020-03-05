using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please, enter valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, enter the first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, enter the last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please, choose the department name")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Please, enter the position")]
        public string PositionName { get; set; }

        [Required(ErrorMessage = "Please, enter reporting manager email")]
        [DataType(DataType.EmailAddress)]
        public string ReportingManagerEmail { get; set; }

        [Required(ErrorMessage = "Please, enter the city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please, enter the date of joining")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required(ErrorMessage = "Please, enter the password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&]).*$", ErrorMessage = "Password must contain at least one uppercase, lowercase, numeric and special character")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 and at max 100 characters long", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not match")]
        public string ConfirmPassword { get; set; }

        public bool SpecialUser { get; set; }
    }
}
