using LexHarvester.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexHarvester.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<LegislationDocument> LegislationDocuments { get; set; }
    public DbSet<CaseLawDocument> CaseLawDocuments { get; set; }
    public DbSet<LegislationType> LegislationTypes { get; set; }
    public DbSet<CaseLawType> CaseLawTypes { get; set; }
    public DbSet<RequestEndpointConfig> RequestEndpointConfigs { get; set; }
    public DbSet<HarvestingState> HarvestingStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegislationDocument>().HasIndex(x => x.LegislationId).IsUnique();
        modelBuilder.Entity<CaseLawDocument>().HasIndex(x => x.DocumentId).IsUnique();
        modelBuilder.Entity<LegislationDocument>().HasIndex(x => x.LegislationTypeId).IsUnique();
        modelBuilder.Entity<CaseLawType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<HarvestingState>().HasIndex(x => x.Id).IsUnique();
    }
}
