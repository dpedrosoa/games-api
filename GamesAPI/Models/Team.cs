namespace GamesAPI.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameTeam> GameTeams { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
