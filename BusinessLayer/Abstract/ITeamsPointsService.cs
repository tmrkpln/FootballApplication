using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITeamsPointsService : IGenericService<TeamPoints>
    {
        void TWinnerTeam(int teamId, int TeamGolCount, int OtherTeamGolCount);
        void TLosingTeam(int teamId, int TeamGolCount, int OtherTeamGolCount);
        void TTie(int HomeOwnerId, int DisplacementID, int HomeOwnerGolCount, int DisplacemenGolCount);
        TeamPoints TGetTeamPoints(int TeamId);
    }
}
