using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RokoAwards.EFDataAccessLibrary.Utils;

namespace RokoAwards.EFDataAccessLibrary.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public DateTime DateOfJoining { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [MaxLength(200)]
        public string PositionName { get; set; }

        [Required]
        [MaxLength(200)]
        public string ReportingManagerEmail { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required]
        public int ImageId { get; set; }
        public Image Image { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Required]
        [MaxLength(100)]
        public string HashedPassword { get; set; }
        public List<UserAward> UserAwardsReceived { get; set; } = new List<UserAward>();
        public List<UserAward> UserAwardsSent { get; set; } = new List<UserAward>();
        public List<Award> CreatedAwards { get; set; }
    }
}
