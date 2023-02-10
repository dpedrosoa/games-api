namespace GamesAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameTeam> GameTeams { get; set; } = new List<GameTeam>();
    }
}
