using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RokoAwards.EFDataAccessLibrary.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(200)]
        public string DepartmentName { get; set; }
        public List<User> Users { get; set; }
    }
}
