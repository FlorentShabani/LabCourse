using Microsoft.EntityFrameworkCore;
using Travista.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Travista.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TravelAgency> TravelAgency { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<Destination> Destination { get; set; }

        public DbSet<TravelTips> TravelTips { get; set; }

        public DbSet<Review> Review { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.ID_Users);

            modelBuilder.Entity<TravelAgency>()
                .HasKey(t => t.ID_TravelAgency);

            modelBuilder.Entity<Country>()
                .HasKey(u => u.ID_Country);

            modelBuilder.Entity<City>()
                .HasKey(u => u.ID_City);

            modelBuilder.Entity<Destination>()
                .HasKey(u => u.ID_Destination);

            modelBuilder.Entity<TravelTips>()
                .HasKey(u => u.ID_TravelTips);

            modelBuilder.Entity<Review>()
                .HasKey(u => u.ID_Reviews);
        }
    }
}
