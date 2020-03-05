using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RokoAwards.EFDataAccessLibrary.Models
{
    public class Image
    {
        public int ImageId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImageName { get; set; }
        
        [Required]
        public string ImagePath { get; set; }
        public List<Award> Awards { get; set; }
        public List<User> Users { get; set; }
    }
}
