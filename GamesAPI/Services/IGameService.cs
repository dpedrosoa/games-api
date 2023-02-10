using GamesAPI.Models;
using GamesAPI.Models.DTO;

namespace GamesAPI.Services
{
    public interface IGameService
    {
        Task<bool> Any(int id);
        Task<GameDto> Get(int id);
        Task<IEnumerable<GameDto>> GetAll();
        Task<bool> Create(GameCreateDto dto);
        Task<bool> Update(GameDto dto);
        Task<bool> Delete(int id);

        Task<List<TeamDto>> GetTeamsByGame(int gameId);
        Task<List<GameTeamInfoDto>> GetTeamsInfoByGame(int gameId);
        Task<bool> AddTeamToGame(int gameId, int teamId);
        Task<bool> DeleteTeamFromGame(int gameId, int teamId);
        Task<bool> UpdateTeamScore(GameTeamDto gameTeamDto);
    }
}
