﻿namespace GamesAPI.Models.DTO
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GameTeamDto> GameTeams { get; set; }
    }
}
