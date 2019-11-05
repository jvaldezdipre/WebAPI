using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BestTeamsApi.Controllers;
using BestTeamsApi.Models;


namespace BestTeamsApi.Models
{
    public class TeamPlayersContext : DbContext
    {
        public TeamPlayersContext(DbContextOptions<TeamPlayersContext> options)
            : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {      
            base.OnModelCreating(modelBuilder);
        }

    }
}