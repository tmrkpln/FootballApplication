using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IWinRateDal : IGenericDal<WinRate>
    {
        void AddWinRate(int homeTeamId, int awayTeamId,int winTeamId);
        WinRate GetByTeamID(int teamId);


    }
}
