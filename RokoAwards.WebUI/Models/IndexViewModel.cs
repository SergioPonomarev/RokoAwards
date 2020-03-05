using RokoAwards.EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RokoAwards.WebUI.Models
{
    public class IndexViewModel
    {
        public IEnumerable<UserAward> UserAwards { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
