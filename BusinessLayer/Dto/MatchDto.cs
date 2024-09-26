
namespace BusinessLayer.Dto
{
    public class MatchDto
    {
        public string HomeOwnerName { get; set; }
        public string DisplacemenName { get; set; }
        public int? DisplacementScore { get; set; }
        public int? HomeOwnerScore { get; set; }
        public List<MatchResultDto> TeamResult { get; set; }
    }
}
