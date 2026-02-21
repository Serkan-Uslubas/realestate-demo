using TopImmo.Api.Domain;

namespace TopImmo.Api.Data;

public static class TopImmoSeedExtensions
{
    public static async Task EnsureSeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TopImmoDbContext>();

        await dbContext.Database.MigrateAsync();

        if (await dbContext.Agents.AnyAsync())
        {
            return;
        }

        var agent = new Agent
        {
            FullName = "Top Immo Team",
            Email = "team@top.immo",
            Phone = "+49 40 0000 0000",
            CreatedAtUtc = DateTime.UtcNow
        };

        var now = DateTime.UtcNow;

        var sampleProperty = new Property
        {
            Slug = "hamburg-eimsbuettel-3-zimmer",
            Title = "3-Zimmer Wohnung in Hamburg-Eimsbuettel",
            Description = "Modernisierte Wohnung mit Balkon und guter Anbindung.",
            City = "Hamburg",
            PostalCode = "20259",
            Street = "Osterstrasse 100",
            OfferType = OfferType.Sale,
            Price = 549000m,
            Bedrooms = 2,
            Bathrooms = 1,
            AreaSqm = 86.5m,
            IsPublished = true,
            Agent = agent,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        dbContext.Agents.Add(agent);
        dbContext.Properties.Add(sampleProperty);

        await dbContext.SaveChangesAsync();
    }
}
