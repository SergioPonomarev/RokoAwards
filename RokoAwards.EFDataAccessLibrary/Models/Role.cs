using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RokoAwards.EFDataAccessLibrary.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
