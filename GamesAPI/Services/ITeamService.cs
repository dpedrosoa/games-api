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
    }
}
