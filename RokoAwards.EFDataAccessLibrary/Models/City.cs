using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RokoAwards.EFDataAccessLibrary.Models
{
    public class City
    {
        public int CityId { get; set; }

        [Required]
        [MaxLength(200)]
        public string CityName { get; set; }
        public List<User> Users { get; set; }
    }
}
