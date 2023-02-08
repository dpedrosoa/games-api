using AutoMapper;
using GamesAPI.Models;
using GamesAPI.Models.DTO;

namespace GamesAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<GameCreateDto, Game>();
            CreateMap<GameDto, Game>();

            CreateMap<GameTeam, GameTeamDto>();
            CreateMap<GameTeamDto, GameTeam>();

            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerCreateDto, Player>();
            CreateMap<PlayerDto, Player>();

            CreateMap<Team, TeamDto>();
            CreateMap<TeamCreateDto, Team>();
            CreateMap<TeamDto, Team>();

        }
    }
}
