using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entites
{
    public class Teams
    {
        [Key]
        public int TeamsID { get; set; }
        public string TeamName { get; set; }
        public DateTime FoundingYear { get; set; }
        public string TeamColours { get; set; }
        public string Logo { get; set; }
        public List<MatchDates> MatchDates { get; set; }

    }
}
