namespace GamesAPI.Models.DTO
{
    public class GameTeamInfoDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int GameId { get; set; }
        public int TeamScore { get; set; }
        public ICollection<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}
