using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RokoAwards.EFDataAccessLibrary.Models
{
    public class UserAward
    {
        public int UserAwardId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AwardIdReceived { get; set; }
        public Award AwardReceived { get; set; }

        public int FromUserId { get; set; }
        public User FromUser { get; set; }

        public int AwardIdSent { get; set; }
        public Award AwardSent { get; set; }

        [Required]
        public DateTime AwardDate { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
