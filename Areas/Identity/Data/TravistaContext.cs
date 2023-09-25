using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Travista.Areas.Identity.Data;
using Travista.Models.Domain;

namespace Travista.Data;

public class TravistaContext : IdentityDbContext<TravistaUser>
{
    private readonly IConfiguration Configuration;
    public TravistaContext()
    {
    }
    public TravistaContext(DbContextOptions<TravistaContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public DbSet<TravistaUser> TravistaUser { get; set; }

    public DbSet<TravelAgency> TravelAgency { get; set; }

    public DbSet<Country> Country { get; set; }

    public DbSet<City> City { get; set; }

    public DbSet<Destination> Destination { get; set; }

    public DbSet<TravelTips> TravelTips { get; set; }

    public DbSet<Review> Review { get; set; }

    public DbSet<Images> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        modelBuilder.ApplyConfiguration(new TravistaUserEntityConfiguration());

        modelBuilder.Entity<TravistaUser>()
            .HasKey(u => u.Id);

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

        modelBuilder.Entity<Images>()
            .HasKey(u => u.ID_Image);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = Configuration.GetConnectionString("TravistaContextConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


}

public class TravistaUserEntityConfiguration : IEntityTypeConfiguration<TravistaUser>
{
    public void Configure(EntityTypeBuilder<TravistaUser> builder)
    {
        builder.Property(u => u.Fullname).HasMaxLength(255);
    }
}