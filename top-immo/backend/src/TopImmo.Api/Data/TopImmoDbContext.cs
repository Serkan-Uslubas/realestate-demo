using TopImmo.Api.Domain;

namespace TopImmo.Api.Data;

public sealed class TopImmoDbContext : DbContext
{
    public TopImmoDbContext(DbContextOptions<TopImmoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Agent> Agents => Set<Agent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable("agents");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.FullName).HasMaxLength(160).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(254).IsRequired();
            entity.Property(x => x.Phone).HasMaxLength(40);
            entity.Property(x => x.CreatedAtUtc).IsRequired();

            entity.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("properties");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Slug).HasMaxLength(180).IsRequired();
            entity.Property(x => x.Title).HasMaxLength(220).IsRequired();
            entity.Property(x => x.Description);
            entity.Property(x => x.City).HasMaxLength(120).IsRequired();
            entity.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Street).HasMaxLength(180).IsRequired();
            entity.Property(x => x.OfferType).HasConversion<int>().IsRequired();
            entity.Property(x => x.Price).HasPrecision(18, 2).IsRequired();
            entity.Property(x => x.Bedrooms).IsRequired();
            entity.Property(x => x.Bathrooms).IsRequired();
            entity.Property(x => x.AreaSqm).HasPrecision(10, 2).IsRequired();
            entity.Property(x => x.IsPublished).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.Property(x => x.UpdatedAtUtc).IsRequired();

            entity.HasIndex(x => x.Slug).IsUnique();
            entity.HasIndex(x => x.City);

            entity
                .HasOne(x => x.Agent)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
