using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Models
{
    public class EditUserModel
    {
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
    }
}
