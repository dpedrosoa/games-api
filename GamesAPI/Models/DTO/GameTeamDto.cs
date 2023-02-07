using GamesAPI.Models.DTO;
using System.Text.Json.Serialization;

namespace GamesAPI.Models
{
    public class GameTeamDto
    {
        public int GameId { get; set; }
        [JsonIgnore]
        public GameDto Game { get; set; }
        public int TeamId { get; set; }
        public TeamDto Team { get; set; }
        public int TeamScore { get; set; }
    }
}
