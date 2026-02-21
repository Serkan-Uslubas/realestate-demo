using TopImmo.Api.Data;
using TopImmo.Api.Domain;
using HotChocolate;

namespace TopImmo.Api.GraphQL.Queries;

public sealed class Query
{
    public async Task<IReadOnlyList<Property>> GetPropertiesAsync(
        [Service] TopImmoDbContext dbContext,
        int take = 24,
        CancellationToken cancellationToken = default)
    {
        var boundedTake = Math.Clamp(take, 1, 100);

        return await dbContext.Properties
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(boundedTake)
            .ToListAsync(cancellationToken);
    }

    public Task<Property?> GetPropertyBySlugAsync(
        [Service] TopImmoDbContext dbContext,
        string slug,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Properties
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<IReadOnlyList<Agent>> GetAgentsAsync(
        [Service] TopImmoDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Agents
            .AsNoTracking()
            .OrderBy(x => x.FullName)
            .ToListAsync(cancellationToken);
    }
}
