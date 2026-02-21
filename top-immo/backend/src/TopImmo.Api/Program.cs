using TopImmo.Api.Data;
using TopImmo.Api.GraphQL.Mutations;
using TopImmo.Api.GraphQL.Queries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TopImmoDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("TopImmoDb"));
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddFiltering()
    .AddSorting();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://127.0.0.1:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();
app.MapGraphQL("/graphql");

app.MapGet("/", () => Results.Ok(new
{
    service = "top.immo API",
    graphql = "/graphql"
}));

await app.EnsureSeedDataAsync();

app.Run();
