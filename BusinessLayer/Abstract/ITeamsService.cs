using EntityLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITeamsService : IGenericService<Teams>
    {
        string TGetTeamName(int id);
    }
}
