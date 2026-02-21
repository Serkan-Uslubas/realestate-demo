using TopImmo.Api.Data;
using TopImmo.Api.Domain;
using TopImmo.Api.GraphQL.Inputs;
using HotChocolate;
using HotChocolate.Types;

namespace TopImmo.Api.GraphQL.Mutations;

public sealed class Mutation
{
    public async Task<Property> CreatePropertyAsync(
        [Service] TopImmoDbContext dbContext,
        CreatePropertyInput input,
        CancellationToken cancellationToken = default)
    {
        var normalizedSlug = input.Slug.Trim().ToLowerInvariant();
        var slugInUse = await dbContext.Properties
            .AnyAsync(x => x.Slug == normalizedSlug, cancellationToken);

        if (slugInUse)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage($"A property with slug '{normalizedSlug}' already exists.")
                    .SetCode("PROPERTY_SLUG_EXISTS")
                    .Build());
        }

        var now = DateTime.UtcNow;

        var property = new Property
        {
            Slug = normalizedSlug,
            Title = input.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(input.Description)
                ? null
                : input.Description.Trim(),
            City = input.City.Trim(),
            PostalCode = input.PostalCode.Trim(),
            Street = input.Street.Trim(),
            OfferType = input.OfferType,
            Price = input.Price,
            Bedrooms = input.Bedrooms,
            Bathrooms = input.Bathrooms,
            AreaSqm = input.AreaSqm,
            IsPublished = input.IsPublished,
            AgentId = input.AgentId,
            CreatedAtUtc = now,
            UpdatedAtUtc = now
        };

        dbContext.Properties.Add(property);
        await dbContext.SaveChangesAsync(cancellationToken);

        return property;
    }
}
