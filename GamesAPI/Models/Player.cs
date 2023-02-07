namespace GamesAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
