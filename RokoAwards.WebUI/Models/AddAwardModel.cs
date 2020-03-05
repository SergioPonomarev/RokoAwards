using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Models
{
    public class AddAwardModel
    {
        [Required]
        public string AwardTitle { get; set; }

        [Required]
        public string RecepientEmail { get; set; }

        [Required]
        public string FromEmail { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
