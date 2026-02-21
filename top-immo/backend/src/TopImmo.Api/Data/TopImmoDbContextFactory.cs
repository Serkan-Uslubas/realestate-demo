namespace TopImmo.Api.Data;

public sealed class TopImmoDbContextFactory : IDesignTimeDbContextFactory<TopImmoDbContext>
{
    public TopImmoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TopImmoDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=topimmo_local;Username=postgres;Password=postgres");

        return new TopImmoDbContext(optionsBuilder.Options);
    }
}
