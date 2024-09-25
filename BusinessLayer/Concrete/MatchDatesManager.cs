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
    public class Match_matchDatesService : IMatchDatesService
    {
        IMatchDatesDal _matchDatesDal;
        ITeamsDal _teamsDal;

        public Match_matchDatesService(IMatchDatesDal matchDatesDal, ITeamsDal teamsDal)
        {
            _matchDatesDal = matchDatesDal;
            _teamsDal = teamsDal;
        }

        public void TAdd(MatchDates entity)
        {
            _matchDatesDal.Add(entity);
        }

        public List<MatchDates> TBringByWeek(int week)
        {
            return _matchDatesDal.BringByWeek(week);
        }

        public void TDelete(MatchDates entity)
        {
            _matchDatesDal.Delete(entity);
        }

        public List<MatchDates> TGetAll()
        {
            return _matchDatesDal.GetAll();
        }

        public MatchDates TGetByID(int id)
        {
            return _matchDatesDal.GetByID(id);
        }

        public List<MatchDates> TGetListTeamName()
        {
            return _matchDatesDal.GetListTeamName();
        }

        public List<int> TGetWeekInfo()
        {
            return _matchDatesDal.GetWeekInfo();
        }

        public void TUpdate(MatchDates entity)
        {
            _matchDatesDal.Update(entity);
        }

        public void TUpdateTeamsScore(int homeTeamId, int awayTeamId, int homeTeamScore, int awayTeamScore, int winning)
        {
            _matchDatesDal.UpdateTeamsScore(homeTeamId, awayTeamId, homeTeamScore, awayTeamScore, winning);
        }

        public void CreateFixture()
        {
            List<Teams> teams = _teamsDal.GetAll();

            // Toplam takım sayısı
            int totalTeams = teams.Count;

            List<MatchDates> matches = new List<MatchDates>();
            DateTime date = DateTime.Now;
            // 18 hafta boyunca maçları organize et
            int num = 0;
            for (int week = 1; week < totalTeams; week++)
            {
                DateTime newDate;
                if (week == 1)
                {
                    newDate = date;
                }
                else
                {
                    newDate = date.AddDays(7);
                    date = newDate;
                }
                // Haftalık eşleşmeler
                List<(Teams, Teams)> weeklyMatches = new List<(Teams, Teams)>();
                for (int i = 0; i < totalTeams / 2; i++)
                {
                    var homeTeam = teams[i];
                    var awayTeam = teams[totalTeams - 1 - i];

                    if (homeTeam.TeamName != "bay" && awayTeam.TeamName != "bay")
                    {
                        weeklyMatches.Add((homeTeam, awayTeam));
                    }
                }

                // Maçları yazdır ve MatchDates nesnelerine kaydet
                foreach (var match in weeklyMatches)
                {
                    // MatchDates nesnesi oluştur ve gerekli verileri ata
                    MatchDates matchRecord = new MatchDates
                    {
                        Weekİnfo = week,
                        MatchDate = newDate.ToString("yyyy-MM-dd"),
                        Displacement = match.Item2.TeamsID,
                        HomeOwner = match.Item1.TeamsID,
                        DisplacementScore = null,
                        HomeOwnerScore = null,
                        Winning = 0,
                    };

                    MatchDates matchRecord1 = new MatchDates
                    {
                        Weekİnfo = totalTeams + num,
                        MatchDate = newDate.AddDays(totalTeams).ToString("yyyy-MM-dd"),
                        Displacement = match.Item1.TeamsID,
                        HomeOwner = match.Item2.TeamsID,
                        DisplacementScore = null,
                        HomeOwnerScore = null,
                        Winning = 0,
                    };
                    matches.Add(matchRecord);
                    matches.Add(matchRecord1);
                }
                num++;
                // Takımları döngüsel olarak kaydır
                Teams lastTeam = teams[totalTeams - 1];
                teams.RemoveAt(totalTeams - 1);
                teams.Insert(1, lastTeam);
            }

            foreach (var team in matches)
            {
                _matchDatesDal.Add(team);
            }
        }
    }
}
