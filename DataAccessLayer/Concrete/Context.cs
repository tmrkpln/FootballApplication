using EntityLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = TAMER\\SQLEXPRESS; database = FootballApplication1; integrated security = true; Trusted_Connection=true;TrustServerCertificate=True");
        }

        public DbSet<Teams> Teams { get; set; }
        public DbSet<TeamPoints> TeamPoints { get; set; }
        public DbSet<MatchDates> MatchDates { get; set; }
        public DbSet<WinRate> WinRates { get; set; }

    }
}

