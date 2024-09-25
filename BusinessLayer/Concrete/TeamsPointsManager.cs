using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TeamsPointsManager : ITeamsPointsService
    {
        ITeamPointsDal _teamsPointDal;

        public TeamsPointsManager(ITeamPointsDal teamsPointDal)
        {
            _teamsPointDal = teamsPointDal;
        }

        public void TAdd(TeamPoints entity)
        {
            _teamsPointDal.Add(entity);
        }

        public void TDelete(TeamPoints entity)
        {
            _teamsPointDal.Delete(entity);
        }

        public List<TeamPoints> TGetAll()
        {
            return _teamsPointDal.GetAll(); 
        }

        public TeamPoints TGetByID(int id)
        {
            return _teamsPointDal.GetByID(id);
        }

        public TeamPoints TGetTeamPoints(int TeamId)
        {
            return _teamsPointDal.GetTeamPoints(TeamId);
        }

        public void TLosingTeam(int teamId, int TeamGolCount, int OtherTeamGolCount)
        {
            _teamsPointDal.LosingTeam(teamId, TeamGolCount, OtherTeamGolCount);
        }

        public void TTie(int HomeOwnerId, int DisplacementID, int HomeOwnerGolCount, int DisplacemenGolCount)
        {
            _teamsPointDal.Tie(HomeOwnerId, DisplacementID, HomeOwnerGolCount, DisplacemenGolCount);
        }

        public void TUpdate(TeamPoints entity)
        {
            _teamsPointDal.Update(entity);
        }

        public void TWinnerTeam(int teamId, int TeamGolCount, int OtherTeamGolCount)
        {
            _teamsPointDal.WinnerTeam(teamId,TeamGolCount, OtherTeamGolCount);
        }
    }
}
