using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Entites;
using FootballApplication1.ModelView;
namespace FootballApplication1.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<MatchDates, MatchDatesViewModel>();
           // .ForMember(d => d.DisplacementName, opt => opt.MapFrom((src, dest) =>
           // {
           //     var team = teamsService.TGetByID(src.Displacement);
           //     return team != null ? team.TeamName : "Unknown";
           // }))
           // .ForMember(d => d.HomeOwnerName, opt => opt.MapFrom((src, dest) =>
           // {
           //     var team = teamsService.TGetByID(src.HomeOwner);
           //     return team != null ? team.TeamName : "Unknown";
           // }))
           // .ForMember(d => d.DisplacementWinRate, opt => opt.MapFrom((src, dest) =>
           // {
           //     var team = winRateService.TGetByTeamID(src.Displacement).Rate;
           //     return team != null ? team : 0;
           // }))
           //.ForMember(d => d.HomeOwnerWinRate, opt => opt.MapFrom((src, dest) =>
           //{
           //    var team = winRateService.TGetByTeamID(src.HomeOwner).Rate;
           //    return team != null ? team : 0;
           //}));
            CreateMap<MatchDatesViewModel, MatchDates>();
        }
    }
}
