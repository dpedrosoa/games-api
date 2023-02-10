using GamesAPI.Models;

namespace GamesAPI.Data.Interfaces
{
    public interface ITeamPlayerRepository
    {
        void Add(TeamPlayer teamPlayer);
        void Delete(TeamPlayer teamPlayer);
        Task<TeamPlayer> Get(int teamId, int playerId);
    }
}
