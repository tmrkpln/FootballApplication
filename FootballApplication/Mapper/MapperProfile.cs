using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Entites;
using FootballApplication1.ModelView;
using BusinessLayer.Dto;
namespace FootballApplication1.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<MatchDates, MatchDatesViewModel>();
            CreateMap<MatchDatesViewModel, MatchDates>();

            CreateMap<MatchDto, MatchViewModel>();
            CreateMap<MatchResultDto, MatchResult>();

            CreateMap<TeamPoints, TeamPointsViewModel>();
            CreateMap<TeamPointsViewModel, TeamPoints>();
        }
    }
}
