using GamesAPI.Models;

namespace GamesAPI.Data.Seed
{
    public class SeedService
    {
        private readonly DataContext _context;

        public SeedService(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.Games.Any() && !_context.Teams.Any() && !_context.Players.Any())
            {
                var commonPlayer = new Player { Name = "Jack" };
                var commonTeam = new Team
                {
                    Name = "The Rangers",
                    TeamPlayers = new List<TeamPlayer>()
                    {
                        new TeamPlayer
                        {
                            Player = new Player{ Name = "Lily" },
                        },
                        new TeamPlayer
                        {
                            Player = new Player{ Name = "Susan" },
                        }
                    }
                };

                var games = new List<Game>()
                {
                    new Game
                    {
                        Name = "Ping pong",
                        GameTeams = new List<GameTeam>()
                        {
                            new GameTeam
                            {
                                Team = commonTeam,
                                TeamScore = 20
                            },
                            new GameTeam
                            {
                                Team = new Team
                                {
                                    Name = "Long pong",
                                    TeamPlayers = new List<TeamPlayer>()
                                    {
                                        new TeamPlayer
                                        {
                                            Player = commonPlayer,
                                        },
                                        new TeamPlayer
                                        {
                                            Player = new Player{ Name = "Taylor" },
                                        }
                                    }
                                },
                                TeamScore = 15
                            }
                        }
                    },
                    new Game
                    {
                        Name = "Football",
                        GameTeams = new List<GameTeam>()
                        {
                            new GameTeam
                            {
                                Team = new Team
                                {
                                    Name = "Best Goals Ever",
                                    TeamPlayers = new List<TeamPlayer>()
                                    {
                                        new TeamPlayer
                                        {
                                            Player = commonPlayer
                                        },
                                        new TeamPlayer
                                        {
                                            Player = new Player { Name = "Alice" },
                                        }
                                    }
                                },
                                TeamScore = 5
                            },
                            new GameTeam
                            {
                                Team = new Team
                                {
                                    Name = "Super Goals",
                                    TeamPlayers = new List<TeamPlayer>()
                                    {
                                        new TeamPlayer
                                        {
                                            Player = new Player { Name = "Tom" },
                                        },
                                        new TeamPlayer
                                        {
                                            Player = new Player { Name = "Mary" },
                                        }
                                    }
                                },
                                TeamScore = 3
                            }
                        }
                    },
                    new Game
                    {
                        Name = "Chess",
                        GameTeams = new List<GameTeam>()
                        {
                            new GameTeam
                            {
                                Team = commonTeam,
                                TeamScore = 10
                            },
                            new GameTeam
                            {
                                Team = new Team
                                {
                                    Name = "IntelliChess",
                                    TeamPlayers = new List<TeamPlayer>()
                                    {
                                        new TeamPlayer
                                        {
                                            Player = commonPlayer,
                                        },
                                        new TeamPlayer
                                        {
                                            Player = new Player { Name = "Karl" },
                                        }
                                    }
                                },
                                TeamScore = 8
                            }
                        }
                    }

                };

                _context.Games.AddRange(games);
                _context.SaveChanges();
            }
        }
    }
}
