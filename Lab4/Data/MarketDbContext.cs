using Assignment02NET.Models;
using Assignment02NET.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Assignment02NET.Data
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions<MarketDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Brokerage> Brokerages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Advertisements> Advertisements { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Brokerage>().ToTable("Brokerages");
            modelBuilder.Entity<Advertisements>().ToTable("Advertisements");
            //modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
            modelBuilder.Entity<Subscription>().HasKey(c => new { c.ClientId, c.BrokerageId });


        }
    }
}
