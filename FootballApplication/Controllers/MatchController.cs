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
using System.Collections.Generic;
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
        public IActionResult CreateScore(int week)
        {
            var matchs = _matchDatesService.TBringByWeek(week);
            foreach (var match in matchs)
            {
                var result = _pointsService.CreateScore(match);
                var matchViewModel = _mapper.Map<MatchViewModel>(result.Item1);
                var matchResults = _mapper.Map<List<MatchResult>>(result.Item2);
                MatchView(matchViewModel, matchResults);
            }
            return RedirectToAction("Index");
        }

        public void MatchView(MatchViewModel matchView, List<MatchResult> matchResults)
        {
            matchView.DisplacementScore = 0;
            matchView.HomeOwnerScore = 0;
            
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

