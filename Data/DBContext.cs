using Microsoft.EntityFrameworkCore;
using Travista.Models.Domain;

namespace Travista.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.ID_Users); // Define primary key
        }
    }
}
