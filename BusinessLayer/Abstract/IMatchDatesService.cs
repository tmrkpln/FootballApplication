using BusinessLayer.Concrete;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMatchDatesService : IGenericService<MatchDates>
    {
        List<int> TGetWeekInfo();
        List<MatchDates> TGetListTeamName();
        void TUpdateTeamsScore(int homeTeamId,int awayTeamId, int homeTeamScore, int awayTeamScore,int winning);
        List<MatchDates> TBringByWeek(int week);
        void CreateFixture();

    }
}
