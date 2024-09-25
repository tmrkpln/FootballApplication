using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Query;
using NuGet.ProjectModel;

namespace FootballApplication1.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamsPointsService _pointsService;
        private readonly ITeamsService _teamsService;
        private readonly IWinRateService _winRateService;

        public TeamController(ITeamsPointsService pointsService, ITeamsService teamsService, IWinRateService winRateService)
        {
            _pointsService = pointsService;
            _teamsService = teamsService;
            _winRateService = winRateService;
        }

        public IActionResult Index()
        {
            var result = _teamsService.TGetAll();
            var filterResult = result.Where(x=> x.TeamName.ToLower() != "bay").ToList();
            var teamCount = result.Count();
            ViewBag.TeamCount = teamCount;
            return View(filterResult);
        }
        [HttpGet]
        public IActionResult TeamAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeamAdd(Teams team, IFormFile LogoFile)
        {
            var files = Request.Form.Files;
            if (LogoFile != null)
            {
                Guid id = Guid.NewGuid();
                // Resmi bir dizine kaydet
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "İmage", id.ToString() + "." + LogoFile.FileName.Split('.')[1]);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(stream);
                }
                team.Logo = "/İmage/" + id.ToString() + "." + LogoFile.FileName.Split('.')[1];
            }
            _teamsService.TAdd(team);

            TeamPoints teamPoints = new TeamPoints()
            {
                TeamID = team.TeamsID,
                Played = 0,
                Wins = 0,
                Losses = 0,
                Draws = 0,
                GoalDifference =0,
                GoalsAgainst = 0,
                GoalsFor = 0,
                Points = 0,
                WinRate = 0,
            };

            _pointsService.TAdd(teamPoints);
            WinRate winRate = new WinRate()
            {
                TeamID = team.TeamsID,
                Rate = 50,
            };
            _winRateService.TAdd(winRate);
            return RedirectToAction("Index");
        }
        [HttpGet]

        public IActionResult UpdateTeam(int id) 
        {
            var result = _teamsService.TGetByID(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTeam(Teams _teams, IFormFile LogoFile)
        {
            var files = Request.Form.Files;
            if (LogoFile != null)
            {
                Guid id = Guid.NewGuid();
                // Resmi bir dizine kaydet
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "İmage", id.ToString() + "." + LogoFile.FileName.Split('.')[1]);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(stream);
                }
                _teams.Logo = "/İmage/" + id.ToString() + "." + LogoFile.FileName.Split('.')[1];
            }
            _teamsService.TUpdate(_teams);
            return RedirectToAction("Index");
        }
        
        public IActionResult DeleteTeam(int id)
        {
            var result = _teamsService.TGetByID(id);
            _teamsService.TDelete(result);
            return RedirectToAction("Index");
        }

    }
}
