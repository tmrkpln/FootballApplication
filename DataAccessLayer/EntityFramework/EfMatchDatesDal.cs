using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMatchDatesDal : GenericRepository<MatchDates>, IMatchDatesDal
    {
        public List<MatchDates> BringByWeek(int week)
        {
            using var context = new Context();
            var values = context.MatchDates.Where(x => x.Weekİnfo == week).ToList();
            return values;

        }

        public List<MatchDates> GetListTeamName()
        {
            using var context = new Context();
            var values = context.MatchDates.Include(x => x.Teams).ToList();
            return values;
        }

        public List<int> GetWeekInfo()
        {
            using var context = new Context();
            //Haflalrı küçükten büyüğe sırala ve tekrarlayanları alma
            var values = context.MatchDates.Select(x => x.Weekİnfo).Distinct().OrderBy(x => x).ToList();
            return values;
        }

        public void UpdateTeamsScore(int homeTeamId, int awayTeamId, int homeTeamScore, int awayTeamScore, int winning)
        {
            using var context = new Context();
            var values = context.MatchDates.Where(x => x.HomeOwner == homeTeamId && x.Displacement == awayTeamId).ToList();
            foreach (var team in values)
            {
                team.HomeOwnerScore = homeTeamScore.ToString();
                team.DisplacementScore = awayTeamScore.ToString();
                team.Winning = winning;
                context.MatchDates.Update(team);
            }
            context.SaveChanges();
        }
    }
}
