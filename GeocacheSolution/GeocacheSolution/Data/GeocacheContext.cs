using GeocacheSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace GeocacheSolution.Data
{
    public class GeocacheContext : DbContext
    {
        public GeocacheContext(DbContextOptions<GeocacheContext> options) : base (options) {}

        public DbSet<Geocache> Geocaches { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geocache>().ToTable("Geocache");
            modelBuilder.Entity<Item>().ToTable("Item");
        }
    }
}
