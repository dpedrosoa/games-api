using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameTeam> GameTeams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Game-Team relation
            modelBuilder.Entity<GameTeam>()
                .HasKey(gt => new { gt.GameId, gt.TeamId });

            modelBuilder.Entity<GameTeam>()
                .HasOne(gt => gt.Game)
                .WithMany(g => g.GameTeams)
                .HasForeignKey(gt => gt.GameId);

            modelBuilder.Entity<GameTeam>()
                .HasOne(gt => gt.Team)
                .WithMany(t => t.GameTeams)
                .HasForeignKey(gt => gt.TeamId);

            // Team-Player relation
            modelBuilder.Entity<TeamPlayer>()
                .HasKey(tp => new { tp.TeamId, tp.PlayerId });

            modelBuilder.Entity<TeamPlayer>()
                .HasOne(tp => tp.Team)
                .WithMany(t => t.TeamPlayers)
                .HasForeignKey(tp => tp.TeamId);

            modelBuilder.Entity<TeamPlayer>()
                .HasOne(tp => tp.Player)
                .WithMany(p => p.TeamPlayers)
                .HasForeignKey(tp => tp.PlayerId);

        }
    }
}
