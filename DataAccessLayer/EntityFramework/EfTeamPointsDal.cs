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
    public class EfTeamPointsDal : GenericRepository<TeamPoints>, ITeamPointsDal
    {
        public TeamPoints GetTeamPoints(int TeamId)
        {
            using var context = new Context();
            var values = context.TeamPoints.Where(x => x.TeamID == TeamId).FirstOrDefault();
            return values;
        }

        public void LosingTeam(int teamId , int TeamGolCount, int OtherTeamGolCount)
        {
            using var context = new Context();
            var values = context.TeamPoints.Where(x => x.TeamID == teamId).FirstOrDefault();
            values.Played += 1;
            values.Losses += 1;
            values.GoalsFor += TeamGolCount;
            values.GoalsAgainst += OtherTeamGolCount;
            values.GoalDifference += TeamGolCount - OtherTeamGolCount;
            context.TeamPoints.Update(values);
            context.SaveChanges();
        }

        public void Tie(int HomeOwnerId, int DisplacementID , int HomeOwnerGolCount, int DisplacemenGolCount)
        {
            using var context = new Context();
            var homeOwner = context.TeamPoints.Where(x=> x.TeamID==HomeOwnerId).FirstOrDefault();
            var displacemen = context.TeamPoints.Where(x=> x.TeamID == DisplacementID).FirstOrDefault();
            homeOwner.Played += 1;
            homeOwner.Draws += 1;
            homeOwner.GoalsFor += HomeOwnerGolCount;
            homeOwner.GoalsAgainst += DisplacemenGolCount;
            homeOwner.Points += 1;
            context.TeamPoints.Update(homeOwner);
            context.SaveChanges();
            displacemen.Played += 1;
            displacemen.Draws += 1;
            displacemen.GoalsFor += HomeOwnerGolCount;
            displacemen.GoalsAgainst += DisplacemenGolCount;
            displacemen.Points += 1;
            context.TeamPoints.Update(displacemen);
            context.SaveChanges();
        }

        public void WinnerTeam(int teamId, int TeamGolCount, int OtherTeamGolCount)
        {
            using var context = new Context();
            var values = context.TeamPoints.Where(x => x.TeamID == teamId).FirstOrDefault();
            values.Played += 1;
            values.Wins += 1;
            values.GoalsFor += TeamGolCount;
            values.GoalsAgainst += OtherTeamGolCount;
            values.GoalDifference += TeamGolCount - OtherTeamGolCount;
            values.Points += 3;
            context.TeamPoints.Update(values);
            context.SaveChanges();
        }
    }
}
