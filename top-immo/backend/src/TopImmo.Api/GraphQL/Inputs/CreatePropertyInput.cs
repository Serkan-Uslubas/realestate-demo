using TopImmo.Api.Domain;

namespace TopImmo.Api.GraphQL.Inputs;

public sealed record CreatePropertyInput(
    string Slug,
    string Title,
    string? Description,
    string City,
    string PostalCode,
    string Street,
    OfferType OfferType,
    decimal Price,
    int Bedrooms,
    int Bathrooms,
    decimal AreaSqm,
    bool IsPublished,
    Guid? AgentId);
