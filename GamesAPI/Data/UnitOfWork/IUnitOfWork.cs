using GamesAPI.Data.Interfaces;
using GamesAPI.Models;

namespace GamesAPI.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Game> GameRepository { get; }
        public IRepository<Player> PlayerRepository { get; }
        public IRepository<Team> TeamRepository { get; }

        public IGameTeamRepository GameTeamRepository { get; }
        Task<bool> Save();   
    }
}
