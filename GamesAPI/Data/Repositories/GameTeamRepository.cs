using GamesAPI.Data.Interfaces;
using GamesAPI.Models;

namespace GamesAPI.Data.Repositories
{
    public class GameTeamRepository : IGameTeamRepository
    {
        private readonly DataContext _context;

        public GameTeamRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(GameTeam gameTeam)
        {
            _context.GameTeams.Add(gameTeam);
        }

        public void Update(GameTeam gameTeam)
        {
            _context.GameTeams.Update(gameTeam);
        }

        public void Delete(GameTeam gameTeam)
        {
            _context.GameTeams.Remove(gameTeam);
        }
    }
}
