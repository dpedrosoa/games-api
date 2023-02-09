using GamesAPI.Models;

namespace GamesAPI.Data.Interfaces
{
    public interface IGameTeamRepository
    {
        void Add(GameTeam gameTeam);
        void Update(GameTeam gameTeam);
        void Delete(GameTeam gameTeam);
    }
}
