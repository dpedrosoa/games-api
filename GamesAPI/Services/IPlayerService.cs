using GamesAPI.Models.DTO;

namespace GamesAPI.Services
{
    public interface IPlayerService
    {
        Task<bool> Any(int id);
        Task<IEnumerable<PlayerDto>> GetAll();
        Task<PlayerDto> Get(int id);
        Task<bool> Create(PlayerCreateDto dto);
        Task<bool> Update(PlayerDto dto);
        Task<bool> Delete(int id);
    }
}
