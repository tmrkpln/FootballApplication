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
    public class TeamsManager : ITeamsService
    {
        private readonly ITeamsDal _teamsDal;

        public TeamsManager(ITeamsDal teamsDal)
        {
            _teamsDal = teamsDal;
        }

        public void TAdd(Teams entity)
        {
            _teamsDal.Add(entity);
        }

        public void TDelete(Teams entity)
        {
            _teamsDal.Delete(entity);
        }

        public List<Teams> TGetAll()
        {
            return _teamsDal.GetAll();
        }

        public Teams TGetByID(int id)
        {
            return _teamsDal.GetByID(id);
        }

        public string TGetTeamName(int id)
        {
            return _teamsDal.GetTeamName(id);
        }

        public void TUpdate(Teams entity)
        {
            _teamsDal.Update(entity);
        }
    }
}
