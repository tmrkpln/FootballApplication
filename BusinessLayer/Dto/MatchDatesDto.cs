namespace BusinessLayer.Dto
{
    public class MatchDatesDto
    {
        public int MatchDatesID { get; set; }
        public int Weekİnfo { get; set; }
        public string MatchDate { get; set; }
        public string DisplacementName { get; set; }
        public int Displacement { get; set; }
        public int HomeOwner { get; set; }
        public string HomeOwnerName { get; set; }
        public string? DisplacementScore { get; set; }
        public string? HomeOwnerScore { get; set; }
        public bool Match { get; set; }
        public int Winning { get; set; }
        public int HomeOwnerWinRate { get; set; }
        public int DisplacementWinRate { get; set; }
    }
}
