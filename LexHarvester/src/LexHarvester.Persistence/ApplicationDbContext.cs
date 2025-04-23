using LexHarvester.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexHarvester.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MevzuatDocument> MevzuatDocuments { get; set; }
    public DbSet<IctihatDocument> IctihatDocuments { get; set; }
    public DbSet<MevzuatType> MevzuatTypes { get; set; }
    public DbSet<IctihatType> IctihatTypes { get; set; }
    public DbSet<RequestEndpointConfig> RequestEndpointConfigs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MevzuatDocument>().HasIndex(x => x.MevzuatId).IsUnique();
        modelBuilder.Entity<IctihatDocument>().HasIndex(x => x.DocumentId).IsUnique();
        modelBuilder.Entity<MevzuatType>().HasIndex(x => x.MevzuatTurId).IsUnique();
        modelBuilder.Entity<IctihatType>().HasIndex(x => x.Name).IsUnique();
    }
}
