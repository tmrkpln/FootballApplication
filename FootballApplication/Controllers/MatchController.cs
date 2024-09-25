using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entites;
using FootballApplication1.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO.Pipelines;
using System.Threading.Channels;
using static System.Formats.Asn1.AsnWriter;

namespace FootballApplication1.Controllers
{
    public class MatchController : Controller
    {

        private readonly ITeamsPointsService _pointsService;
        private readonly ITeamsService _teamsService;
        private readonly IWinRateService _winRateService;
        private readonly IMatchDatesService _matchDatesService;
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly IMapper _mapper;

        public MatchController(ITeamsPointsService pointsService, ITeamsService teamsService, IWinRateService winRateService, IMatchDatesService matchDatesService, IHubContext<SignalRHub> hubContext, IMapper mapper)
        {
            _pointsService = pointsService;
            _teamsService = teamsService;
            _winRateService = winRateService;
            _matchDatesService = matchDatesService;
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public static List<MatchResult> teamResult = new List<MatchResult>();
        public IActionResult Index()
        {
            ViewBag.v1 = _matchDatesService.TGetWeekInfo();

            var matchDates = _matchDatesService.TGetAll();
            List<MatchDatesViewModel> result = new List<MatchDatesViewModel>();   
            foreach (var item in matchDates)
            {
                var displacementTeam = _teamsService.TGetByID(item.Displacement);
                var homeOwnerTeam = _teamsService.TGetByID(item.HomeOwner);

                var homeRate = _winRateService.TGetByTeamID(item.HomeOwner).Rate;
                var displacementRate = _winRateService.TGetByTeamID(item.Displacement).Rate;

                var mappedItem = _mapper.Map<MatchDatesViewModel>(item);
                mappedItem.DisplacementName = displacementTeam?.TeamName ?? "Unknown";
                mappedItem.HomeOwnerName = homeOwnerTeam?.TeamName ?? "Unknown";
                mappedItem.DisplacementWinRate = displacementRate;
                mappedItem.HomeOwnerWinRate = homeRate;

                result.Add(mappedItem);
            }

            return View(result);
        }

        public IActionResult CreateFixture()
        {
            _matchDatesService.CreateFixture();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateScore(int week)
        {
            //bussines
            var random = new Random();
            var matchs = _matchDatesService.TBringByWeek(week);

            foreach (var match in matchs)
            {
                var winRateHomeOwner = _winRateService.TGetByTeamID(match.HomeOwner).Rate;
                var winRateDisplacement = _winRateService.TGetByTeamID(match.Displacement).Rate;
                int? homeTeamGoalsFor = _pointsService.TGetTeamPoints(match.HomeOwner).GoalsFor;
                int? DisplacementGoalsFor = _pointsService.TGetTeamPoints(match.Displacement).GoalsFor;
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
                    };

                if (match.Weekİnfo == 1)
                {
                    var homeTeamScore = random.Next(0, 5);
                    var awayTeamScore = random.Next(0, 5);
                    ScoreAdd(match.HomeOwner, match.Displacement, homeTeamScore, awayTeamScore);
                }
                else
                {

                    int homeTeamScore = 0, awayTeamScore = 0;
                    List<MatchResult> resultHome = new List<MatchResult>();
                    List<MatchResult> resultAway = new List<MatchResult>();
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
                    MatchViewModel viewModel = new MatchViewModel
                    {
                        DisplacemenName = awayTeamName,
                        HomeOwnerName = homeTeamName,
                        DisplacementScore = 0,
                        HomeOwnerScore = 0,
                    };
                    ScoreAdd(match.HomeOwner, match.Displacement, homeTeamScore, awayTeamScore);
                    MatchView(teamResult, viewModel);
                }
            }
            return RedirectToAction("Index");

        }
        public int TeamScoreResult(List<MatchResult> matchResults)
        {
            //bussines
            int result = 0;
            for (int i = 0; i < matchResults.Count; i++)
            {
                result += matchResults[i].Score;
            }
            return result;
        }
        public List<MatchResult> TotalList(List<MatchResult> matchResultsHome, List<MatchResult> matchResultsAway)
        {
            //bussines
            List<MatchResult> matches = new List<MatchResult>();

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
        public List<MatchResult> TeamScore(List<string> attackTypes, int attackRate, int chanceRate, string TeamName)
        {
            //bussines
            List<MatchResult> strings = new List<MatchResult>();
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
                    new MatchResult
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
            //bussines
            int winning;
            if (homeTeamScore > awayTeamScore)
            {

                _pointsService.TWinnerTeam(HomeOwnerID, homeTeamScore, awayTeamScore);
                _pointsService.TLosingTeam(DisplacementID, awayTeamScore, homeTeamScore);
                winning = HomeOwnerID;
            }
            else if (homeTeamScore == awayTeamScore)
            {
                winning = 0;
                _pointsService.TTie(HomeOwnerID, DisplacementID, homeTeamScore, awayTeamScore);
            }
            else
            {
                _pointsService.TLosingTeam(HomeOwnerID, homeTeamScore, awayTeamScore);
                _pointsService.TWinnerTeam(DisplacementID, awayTeamScore, homeTeamScore);
                winning = DisplacementID;
            }
            _winRateService.TAddWinRate(HomeOwnerID, DisplacementID, winning);
            _matchDatesService.TUpdateTeamsScore(HomeOwnerID, DisplacementID, homeTeamScore, awayTeamScore, winning);
        }

        public void MatchView(List<MatchResult> matchResults, MatchViewModel matchView)
        {
            //bussines
            //_hubContext.Clients.All.SendAsync("sendWeek", matchView.Week);
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Maç Başladı!");

            _hubContext.Clients.All.SendAsync("ReceiveMatchDatesList", matchView);
            Thread.Sleep(2000);
            Random random = new Random();
            int index = matchResults.Count();
            for (int i = 0; i < matchResults.Count(); i++)
            {

                int num = random.Next(0, index);
                if (matchResults[num].TeamName == matchView.HomeOwnerName)
                {
                    matchView.HomeOwnerScore += matchResults[num].Score;
                    _hubContext.Clients.All.SendAsync("ReceiveAttackList", matchResults[num]);
                    Thread.Sleep(2000);
                    _hubContext.Clients.All.SendAsync("ReceiveAttackResult", matchResults[num].AttackResult);
                    matchResults.RemoveAt(num);
                    index--;
                }
                else
                {
                    matchView.DisplacementScore += matchResults[num].Score;
                    _hubContext.Clients.All.SendAsync("ReceiveAttackList", matchResults[num]);
                    Thread.Sleep(2000);
                    _hubContext.Clients.All.SendAsync("ReceiveAttackResult", matchResults[num].AttackResult);
                    matchResults.RemoveAt(num);
                    index--;
                }
                Thread.Sleep(2000);
                _hubContext.Clients.All.SendAsync("ReceiveMatchDatesList", matchView);
                Thread.Sleep(5000);
                _hubContext.Clients.All.SendAsync("ReceiveMessage", "Müsabaka devam ediyor...");
            }

            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Maç bitti!");

            Thread.Sleep(2000);


        }
    }

}

