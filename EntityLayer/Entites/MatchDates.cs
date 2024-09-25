using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entites
{
    public class MatchDates
    {
        [Key]
        public int MatchDatesID { get; set; }
        public int Weekİnfo { get; set; }
        public string MatchDate { get; set; }
        public int Displacement { get; set; }
        public int HomeOwner { get; set; }
        public string? DisplacementScore { get; set;}
        public string? HomeOwnerScore { get; set;}
        public bool Match { get; set; }
        public int Winning { get; set;}
        public List<Teams> Teams { get; set; }

    }
}
