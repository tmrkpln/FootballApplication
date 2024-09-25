using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entites
{
    public class TeamPoints
    {
        [Key]
        public int TeamPointsID { get; set; }
        public string? League { get; set; } //Lig
        public int? Played { get; set; } // Oynanan Maç Sayısı
        public int? Wins { get; set; }   // Galibiyet Sayısı
        public int? Draws { get; set; }  // Beraberlik Sayısı
        public int? Losses { get; set; } // Mağlubiyet Sayısı
        public int? GoalsFor { get; set; } // Atılan Gol Sayısı
        public int? GoalsAgainst { get; set; } // Yenilen Gol Sayısı
        public int? GoalDifference { get; set; } // Averaj (Atılan Gol - Yenilen Gol)
        public int? Points { get; set; } // Puan
        public double? WinRate { get; set; } // Galibiyet oranı (Yüzde)
        public Teams Team { get; set; } // İlişkilendirilmiş Takım Nesnesi
        public int TeamID { get; set; } // İlişkilendirilmiş Takım Nesnesi

    }
}
