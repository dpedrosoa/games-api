using GamesAPI.Models;
using GamesAPI.Models.DTO;

namespace GamesAPI.Services
{
    public interface IGameService
    {
        Task<bool> Any(int id);
        Task<GameDto> Get(int id);
        Task<IEnumerable<GameDto>> GetAll();
        Task<bool> Create(GameCreateDto game);
        Task<bool> Update(GameUpdateDto game);
        Task<bool> Delete(int id);
    }
}
