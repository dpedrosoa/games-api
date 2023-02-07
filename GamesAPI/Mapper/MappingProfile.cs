using AutoMapper;
using GamesAPI.Models;
using GamesAPI.Models.DTO;

namespace GamesAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();

            CreateMap<Game, GameDto>();
            CreateMap<GameCreateDto, Game>();
            CreateMap<GameUpdateDto, Game>();


            CreateMap<GameTeam, GameTeamDto>();
            CreateMap<GameTeamDto, GameTeam>();

        }
    }
}
