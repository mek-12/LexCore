using LexHarvester.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LexHarvester.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<LegislationDocumentReference> LegislationDocumentReferences { get; set; }
    public DbSet<CaseLawDocumentReference> CaseLawDocumentReferences { get; set; }
    public DbSet<LegislationType> LegislationTypes { get; set; }
    public DbSet<CaseLawType> CaseLawTypes { get; set; }
    public DbSet<RequestEndpointConfig> RequestEndpointConfigs { get; set; }
    public DbSet<HarvestingState> HarvestingStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegislationDocumentReference>().ToTable(nameof(LegislationDocumentReference));
        modelBuilder.Entity<CaseLawDocumentReference>().ToTable(nameof(CaseLawDocumentReference));

        modelBuilder.Entity<LegislationDocumentReference>().HasIndex(x => x.LegislationId).IsUnique();
        modelBuilder.Entity<CaseLawDocumentReference>().HasIndex(x => x.DocumentId).IsUnique();
        modelBuilder.Entity<CaseLawType>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<HarvestingState>().HasIndex(x => x.Id).IsUnique();
    }
}
