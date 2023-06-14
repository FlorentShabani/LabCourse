using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Travista.Areas.Identity.Data;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new TravistaUserEntityConfiguration());
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