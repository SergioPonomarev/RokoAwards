using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RokoAwards.EFDataAccessLibrary.Utils;

namespace RokoAwards.EFDataAccessLibrary.Models
{
    public class Award
    {
        public int AwardId { get; set; }

        [Required]
        [MaxLength(200)]
        public string AwardTitle { get; set; }

        [Required]
        public int CreaterId { get; set; }
        public User Creater { get; set; }

        [Required]
        public DateTime CreatingDate { get; set; }

        [Required]
        public AwardType AwardType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ImageId { get; set; }
        public Image Image { get; set; }

        public List<UserAward> UserAwardsReceived { get; set; } = new List<UserAward>();
        public List<UserAward> UserAwardsSent { get; set; } = new List<UserAward>();
    }
}
