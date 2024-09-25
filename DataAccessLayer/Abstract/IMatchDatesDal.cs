using EntityLayer.Entites;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMatchDatesDal : IGenericDal<MatchDates>
    {
        List<int> GetWeekInfo();
        List<MatchDates> GetListTeamName();
        void UpdateTeamsScore(int homeTeamId, int awayTeamId, int homeTeamScore, int awayTeamScore, int winning);
        List<MatchDates> BringByWeek(int week);

    }
}
