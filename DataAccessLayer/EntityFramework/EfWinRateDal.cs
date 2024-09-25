using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfWinRateDal : GenericRepository<WinRate>, IWinRateDal
    {
        public void AddWinRate(int homeTeamId, int awayTeamId, int winTeamId)
        {
            using var context = new Context();
            var homeTeam = context.WinRates.Where(x => x.TeamID == homeTeamId).FirstOrDefault();
            var awayTeam = context.WinRates.Where(x => x.TeamID == awayTeamId).FirstOrDefault();
            if (homeTeamId == winTeamId)
            {
                homeTeam.Rate += 2;
                if (awayTeam.Rate - 2 < 0)
                {
                    awayTeam.Rate = 0;
                }
                else
                {
                    awayTeam.Rate -= 2;

                }


            }
            else if (awayTeamId == winTeamId)
            {
                awayTeam.Rate += 3;
                if (homeTeam.Rate - 2 < 0)
                {
                    homeTeam.Rate = 0;
                }
                else
                {
                    homeTeam.Rate -= 3;

                }
            }
            context.WinRates.Update(homeTeam);
            context.WinRates.Update(awayTeam);
            context.SaveChanges();
        }

        public WinRate GetByTeamID(int teamId)
        {
            using var context = new Context();
            var values = context.WinRates.Where(x => x.TeamID == teamId).FirstOrDefault();
            return values;
        }
    }
}
