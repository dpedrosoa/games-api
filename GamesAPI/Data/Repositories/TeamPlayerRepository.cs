using GamesAPI.Data.Interfaces;
using GamesAPI.Models;

namespace GamesAPI.Data.Repositories
{
    public class TeamPlayerRepository : ITeamPlayerRepository
    {
        private readonly DataContext _context;

        public TeamPlayerRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(TeamPlayer teamPlayer)
        {
            _context.TeamPlayers.Add(teamPlayer);
        }

        public void Delete(TeamPlayer teamPlayer)
        {
            _context.TeamPlayers.Remove(teamPlayer);
        }

        public async Task<TeamPlayer> Get(int teamId, int playerId)
        {
            return await _context.TeamPlayers.FindAsync(teamId, playerId);
        }

    }
}
