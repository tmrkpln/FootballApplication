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
    public class EfTeamsDal : GenericRepository<Teams>, ITeamsDal
    {
        public string GetTeamName(int id)
        {
            using var context = new Context();
            var values = context.Teams.Where(x => x.TeamsID == id).Select(x => x.TeamName).FirstOrDefault();
            return values;
        }
    }
}
