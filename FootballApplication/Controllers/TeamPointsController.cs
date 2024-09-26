using AutoMapper;
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
        private readonly IMapper _mapper;

        public TeamPointsController(ITeamsService teamsService, ITeamsPointsService pointsService,IMapper mapper)
        {
            _teamsService = teamsService;
            _pointsService = pointsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var values = _pointsService.TGetAll();
            List<TeamPointsViewModel> teamsPoints = new List<TeamPointsViewModel>();
            foreach (var teamPoint in values)
            {
                var mappedItem = _mapper.Map<TeamPointsViewModel>(teamPoint);
                mappedItem.TeamName = _teamsService.TGetByID(teamPoint.TeamID).TeamName;
                teamsPoints.Add(mappedItem);
            }
            teamsPoints = teamsPoints.OrderByDescending(tp => tp.Points).ToList();
            return View(teamsPoints);
        }
    }
}
