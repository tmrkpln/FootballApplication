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
    public class Win_winRateService : IWinRateService
    {
        IWinRateDal _winRateDal;

        public Win_winRateService(IWinRateDal winRateDal)
        {
            _winRateDal = winRateDal;
        }

        public void TAdd(WinRate entity)
        {
            _winRateDal.Add(entity);
        }

        public void TAddWinRate(int homeTeamId, int awayTeamId, int winTeamId)
        {
            _winRateDal.AddWinRate(homeTeamId, awayTeamId, winTeamId);
        }

        public void TDelete(WinRate entity)
        {
            _winRateDal.Delete(entity);
        }

        public List<WinRate> TGetAll()
        {
            return _winRateDal.GetAll();    
        }

        public WinRate TGetByID(int id)
        {
            return _winRateDal.GetByID(id);
        }

        public WinRate TGetByTeamID(int teamId)
        {
            return _winRateDal.GetByTeamID(teamId);
        }

        public void TUpdate(WinRate entity)
        {
            _winRateDal.Update(entity);
        }
    }
}
