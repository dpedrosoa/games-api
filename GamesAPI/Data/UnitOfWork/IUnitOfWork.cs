using GamesAPI.Data.Interfaces;
using GamesAPI.Models;

namespace GamesAPI.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Game> GameRepository { get; }

        Task<bool> Save();   
    }
}
