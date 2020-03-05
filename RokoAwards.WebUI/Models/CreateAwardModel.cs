using RokoAwards.EFDataAccessLibrary.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Models
{
    public class CreateAwardModel
    {
        [Required]
        [MaxLength(200)]
        public string AwardTitle { get; set; }

        [Required]
        [Range((int)(AwardType.Award), (int)(AwardType.Thanks), ErrorMessage = "Please choose award type")]
        public AwardType AwardType { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
