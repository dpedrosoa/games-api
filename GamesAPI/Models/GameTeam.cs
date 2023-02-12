namespace GamesAPI.Models
{
    public class GameTeam
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int TeamScore { get; set; } = 0;
    }
}
