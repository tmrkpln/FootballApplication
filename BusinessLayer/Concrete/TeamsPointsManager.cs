using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entites;
using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Concrete
{
    public class TeamsPointsManager : ITeamsPointsService
    {
        ITeamPointsDal _teamsPointDal;
        IMatchDatesService _matchDatesService;
        IWinRateService _winRateService;
        ITeamsService _teamsService;

        public static List<MatchResultDto> teamResult = new List<MatchResultDto>();

        public TeamsPointsManager(ITeamPointsDal teamsPointDal, IMatchDatesService matchDatesService, IWinRateService winRateService, ITeamsService teamsService)
        {
            _teamsPointDal = teamsPointDal;
            _matchDatesService = matchDatesService;
            _winRateService = winRateService;
            _teamsService = teamsService;
        }

        public void TAdd(TeamPoints entity)
        {
            _teamsPointDal.Add(entity);
        }

        public void TDelete(TeamPoints entity)
        {
            _teamsPointDal.Delete(entity);
        }

        public List<TeamPoints> TGetAll()
        {
            return _teamsPointDal.GetAll();
        }

        public TeamPoints TGetByID(int id)
        {
            return _teamsPointDal.GetByID(id);
        }

        public TeamPoints TGetTeamPoints(int TeamId)
        {
            return _teamsPointDal.GetTeamPoints(TeamId);
        }

        public void TLosingTeam(int teamId, int TeamGolCount, int OtherTeamGolCount)
        {
            _teamsPointDal.LosingTeam(teamId, TeamGolCount, OtherTeamGolCount);
        }

        public void TTie(int HomeOwnerId, int DisplacementID, int HomeOwnerGolCount, int DisplacemenGolCount)
        {
            _teamsPointDal.Tie(HomeOwnerId, DisplacementID, HomeOwnerGolCount, DisplacemenGolCount);
        }

        public void TUpdate(TeamPoints entity)
        {
            _teamsPointDal.Update(entity);
        }

        public void TWinnerTeam(int teamId, int TeamGolCount, int OtherTeamGolCount)
        {
            _teamsPointDal.WinnerTeam(teamId, TeamGolCount, OtherTeamGolCount);
        }

        public (MatchDto, List<MatchResultDto>) CreateScore(MatchDates match)
        {
            var random = new Random();
            MatchDto viewModel = new MatchDto();
            List<MatchResultDto> teamResult = new List<MatchResultDto>();
            var winRateHomeOwner = _winRateService.TGetByTeamID(match.HomeOwner).Rate;
            var winRateDisplacement = _winRateService.TGetByTeamID(match.Displacement).Rate;

            // Eğer iki takımın gol sayıları için durum kontrolü gerekiyorsa
            int? homeTeamGoalsFor = _teamsPointDal.GetTeamPoints(match.HomeOwner).GoalsFor;
            int? displacementGoalsFor = _teamsPointDal.GetTeamPoints(match.Displacement).GoalsFor;
            var homeTeamName = _teamsService.TGetTeamName(match.HomeOwner);
            var awayTeamName = _teamsService.TGetTeamName(match.Displacement);

            List<string> attackTypes = new List<string>
                    {
                        "Ceza Sahası İçi Şut",
                        "Kafa Vuruşu",
                        "Penaltı",
                        "Frikik",
                        "Karşı Karşıya Kaleciyle",
                        "Korner",
                        "Ara Pası Sonrası Kaleciyle Karşı Karşıya",
                        "Kanat Ortası ve Şut",
                        "Dribbling Sonrası Şut",
                        "Kontratak Sonrası Şut",
                        "Serbeş Vuruş"
                    };

            if (match.Weekİnfo == 1)
            {
                var homeTeamScore = random.Next(0, 5);
                var awayTeamScore = random.Next(0, 5);
                ScoreAdd(match.HomeOwner, match.Displacement, homeTeamScore, awayTeamScore);
                viewModel = new MatchDto
                {
                    DisplacemenName = awayTeamName,
                    HomeOwnerName = homeTeamName,
                    DisplacementScore = awayTeamScore,
                    HomeOwnerScore = homeTeamScore,
                };
            }
            else
            {
                int homeTeamScore = 0, awayTeamScore = 0;
                List<MatchResultDto> resultHome = new List<MatchResultDto>();
                List<MatchResultDto> resultAway = new List<MatchResultDto>();

                if (winRateHomeOwner > winRateDisplacement)
                {
                    resultHome = TeamScore(attackTypes, 2, 5, homeTeamName);
                    resultAway = TeamScore(attackTypes, 0, 0, awayTeamName);
                }
                else
                {
                    resultHome = TeamScore(attackTypes, 0, 0, homeTeamName);
                    resultAway = TeamScore(attackTypes, 2, 5, awayTeamName);
                }

                teamResult = TotalList(resultHome, resultAway);
                homeTeamScore += TeamScoreResult(resultHome);
                awayTeamScore += TeamScoreResult(resultAway);

                viewModel = new MatchDto
                {
                    DisplacemenName = awayTeamName,
                    HomeOwnerName = homeTeamName,
                    DisplacementScore = awayTeamScore,
                    HomeOwnerScore = homeTeamScore,
                };
                ScoreAdd(match.HomeOwner, match.Displacement, homeTeamScore, awayTeamScore);
                return (viewModel, teamResult);
            }
            return (viewModel, teamResult);
        }
        public List<MatchResultDto> TeamScore(List<string> attackTypes, int attackRate, int chanceRate, string TeamName)
        {
            //bussines
            List<MatchResultDto> strings = new List<MatchResultDto>();
            var random = new Random();
            string attackType = "";
            int attack = random.Next(1, 10);
            attack += attackRate;
            int teamScore = 0;
            for (int i = 0; i < attack; i++)
            {
                int num = random.Next(0, attack);
                attackType = attackTypes[num];
                var result = AttackResult(attackType, chanceRate);
                var score = result.Item1;
                var message = result.Item2;
                strings.Add(
                    new MatchResultDto
                    {
                        AttackName = attackType,
                        Score = score,
                        TeamName = TeamName,
                        AttackResult = message,
                    }
                );

            }
            return strings;
        }
        public List<MatchResultDto> TotalList(List<MatchResultDto> matchResultsHome, List<MatchResultDto> matchResultsAway)
        {
            //bussines
            List<MatchResultDto> matches = new List<MatchResultDto>();

            for (int i = 0; i < matchResultsHome.Count; i++)
            {
                matches.Add(matchResultsHome[i]);
            }
            for (int i = 0; i < matchResultsAway.Count; i++)
            {
                matches.Add(matchResultsAway[i]);
            }
            return matches;
        }
        public int TeamScoreResult(List<MatchResultDto> matchResults)
        {
            int result = 0;
            for (int i = 0; i < matchResults.Count; i++)
            {
                result += matchResults[i].Score;
            }
            return result;
        }
        public (int, string) AttackResult(string attackType, int chanceRate)
        {
            //bussines
            var random = new Random();
            int chance = random.Next(100);
            var missMessages = new List<string> { "Kaleci kurtardı!", "Top dışarı çıktı!", "Direkten döndü!" };
            if (attackType == "Penaltı")
            {
                if (chance > 20 + chanceRate)
                    return (1, "Gol!");
                else
                {
                    //var missMessages = new List<string> { "Kaleci kurtardı!", "Top dışarı çıktı!", "Direkten döndü!" };
                    return (0, missMessages[random.Next(missMessages.Count)]);
                }
            }
            else if (attackType == "Ceza Sahası İçi Şut" || attackType == "Karşı Karşıya Kaleciyle")
            {
                if (chance > 30 + chanceRate)
                    return (1, "Gol!");
                else
                {
                    return (0, missMessages[random.Next(missMessages.Count)]);
                }
            }
            else if (attackType == "Kafa Vuruşu" || attackType == "Frikik")
            {
                if (chance > 50 + chanceRate)
                    return (1, "Gol!");
                else
                {
                    return (0, missMessages[random.Next(missMessages.Count)]);
                }
            }
            else
            {
                if (chance > 80 + chanceRate)
                    return (1, "Gol!");
                else
                {
                    return (0, missMessages[random.Next(missMessages.Count)]);
                }
            }
        }
        public void ScoreAdd(int HomeOwnerID, int DisplacementID, int homeTeamScore, int awayTeamScore)
        {
            int winning;
            if (homeTeamScore > awayTeamScore)
            {

                _teamsPointDal.WinnerTeam(HomeOwnerID, homeTeamScore, awayTeamScore);
                _teamsPointDal.LosingTeam(DisplacementID, awayTeamScore, homeTeamScore);
                winning = HomeOwnerID;
            }
            else if (homeTeamScore == awayTeamScore)
            {
                winning = 0;
                _teamsPointDal.Tie(HomeOwnerID, DisplacementID, homeTeamScore, awayTeamScore);
            }
            else
            {
                _teamsPointDal.LosingTeam(HomeOwnerID, homeTeamScore, awayTeamScore);
                _teamsPointDal.WinnerTeam(DisplacementID, awayTeamScore, homeTeamScore);
                winning = DisplacementID;
            }
            _winRateService.TAddWinRate(HomeOwnerID, DisplacementID, winning);
            _matchDatesService.TUpdateTeamsScore(HomeOwnerID, DisplacementID, homeTeamScore, awayTeamScore, winning);
        }
    }
}
