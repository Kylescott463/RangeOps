using System.Net;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;

namespace RangeOps.IntegrationTests.Health;

public sealed class ReadinessTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres =
        new PostgreSqlBuilder("postgres:18-alpine").Build();

    private WebApplicationFactory<Program>? _factory;

    [Fact]
    public async Task ReadyWithPostgreSqlReturnsHealthy()
    {
        using var client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
        });

        var response = await client.GetAsync("/health/ready");
        using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        payload.RootElement.GetProperty("status").GetString().Should().Be("Healthy");
    }

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder.UseSetting(
                    "ConnectionStrings:RangeOps",
                    _postgres.GetConnectionString()));
    }

    public async Task DisposeAsync()
    {
        _factory?.Dispose();
        await _postgres.DisposeAsync();
    }

    private WebApplicationFactory<Program> Factory =>
        _factory ?? throw new InvalidOperationException("The test API has not been initialized.");
}
