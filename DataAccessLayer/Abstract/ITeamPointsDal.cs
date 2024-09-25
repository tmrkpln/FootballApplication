using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITeamPointsDal : IGenericDal<TeamPoints>
    {
        void WinnerTeam(int teamId, int TeamGolCount, int OtherTeamGolCount);
        void LosingTeam(int teamId, int TeamGolCount, int OtherTeamGolCount);
        void Tie(int HomeOwnerId, int DisplacementID, int HomeOwnerGolCount, int DisplacemenGolCount);
        TeamPoints GetTeamPoints(int TeamId);
    }
}
