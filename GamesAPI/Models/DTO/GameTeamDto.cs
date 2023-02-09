using GamesAPI.Models.DTO;
using System.Text.Json.Serialization;

namespace GamesAPI.Models
{
    public class GameTeamDto
    {
        public int GameId { get; set; }
        public int TeamId { get; set; }
        public int TeamScore { get; set; } = 0;
    }
}
