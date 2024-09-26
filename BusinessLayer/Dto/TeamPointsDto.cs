namespace BusinessLayer.Dto
{
    public class TeamPointsDto
    {
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
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}
