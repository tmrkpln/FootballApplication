using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entites;
using FootballApplication1.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication1.Controllers
{
    public class TeamPointsController : Controller
    {

        private readonly ITeamsService _teamsService;
        private readonly ITeamsPointsService _pointsService;

        public TeamPointsController(ITeamsService teamsService, ITeamsPointsService pointsService)
        {
            _teamsService = teamsService;
            _pointsService = pointsService;
        }

        public IActionResult Index()
        {
            var values = _pointsService.TGetAll();
            List<_pointsServiceViewModel> teamsPoints = new List<_pointsServiceViewModel>();
            foreach (var teamPoint in values)
            {
                teamsPoints.Add(new _pointsServiceViewModel()
                {
                    _pointsServiceID = teamPoint.TeamPointsID,
                    Draws = teamPoint.Draws,
                    GoalDifference = teamPoint.GoalDifference,
                    GoalsAgainst = teamPoint.GoalsAgainst,
                    GoalsFor = teamPoint.GoalsFor,
                    League = teamPoint.League,
                    Losses = teamPoint.Losses,
                    Played = teamPoint.Played,
                    Points = teamPoint.Points,
                    Wins = teamPoint.Wins,
                    TeamId = teamPoint.TeamID,
                    TeamName = _teamsService.TGetByID(teamPoint.TeamID).TeamName,
                });
            }
            teamsPoints = teamsPoints.OrderByDescending(tp => tp.Points).ToList();
            return View(teamsPoints);
        }
    }
}
