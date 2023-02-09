using GamesAPI.Data.Interfaces;
using GamesAPI.Data.Repositories;
using GamesAPI.Models;

namespace GamesAPI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        #region Repositories

        private IRepository<Game> _GameRepository;
        public IRepository<Game> GameRepository => _GameRepository ?? new Repository<Game>(_context);


        private IRepository<Player> _PlayerRepository;
        public IRepository<Player> PlayerRepository => _PlayerRepository ?? new Repository<Player>(_context);


        private IRepository<Team> _TeamRepository;
        public IRepository<Team> TeamRepository => _TeamRepository ?? new Repository<Team>(_context);


        private IGameTeamRepository _GameTeamRepository;
        public IGameTeamRepository GameTeamRepository => _GameTeamRepository ?? new GameTeamRepository(_context);

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> Save()
        {
            int saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
