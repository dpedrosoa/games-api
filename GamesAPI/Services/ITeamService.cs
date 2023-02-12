using GamesAPI.Models.DTO;

namespace GamesAPI.Services
{
    public interface ITeamService
    {
        Task<bool> Any(int id);
        Task<IEnumerable<TeamDto>> GetAll();
        Task<TeamDto> Get(int id);
        Task<bool> Create(TeamCreateDto dto);
        Task<bool> Update(TeamDto dto);
        Task<bool> Delete(int id);

        Task<List<PlayerDto>> GetPlayersByTeam(int teamId);
        Task<bool> AddPlayerToTeam(int teamId, int playerId);
        Task<bool> DeletePlayerFromTeam(int teamId, int playerId);
    }
}
