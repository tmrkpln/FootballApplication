using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IWinRateService : IGenericService<WinRate>
    {
        void TAddWinRate(int homeTeamId, int awayTeamId, int winTeamId);
        WinRate TGetByTeamID(int teamId);
    }
}
