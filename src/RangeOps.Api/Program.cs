using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RangeOps.Api.HealthChecks;
using RangeOps.Infrastructure;
using RangeOps.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

const string LivenessTag = "live";
const string ReadinessTag = "ready";

var databaseConnectionString = builder.Configuration.GetConnectionString("RangeOps")
    ?? throw new InvalidOperationException(
        "Connection string 'RangeOps' is not configured. See docs/setup/local-development.md.");

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "RangeOps Mission Readiness API";
        document.Info.Version = "v1";
        document.Info.Description =
            "API for managing assets, maintenance, and mission readiness.";

        return Task.CompletedTask;
    });
});
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = context.HttpContext.Request.Path;
        context.ProblemDetails.Extensions["traceId"] =
            Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
    };
});
builder.Services.AddInfrastructure(databaseConnectionString);
builder.Services
    .AddHealthChecks()
    .AddCheck(
        "self",
        () => HealthCheckResult.Healthy(),
        tags: [LivenessTag])
    .AddDbContextCheck<RangeOpsDbContext>(
        "postgresql",
        tags: [ReadinessTag]);

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "RangeOps API v1");
        options.DocumentTitle = "RangeOps Mission Readiness API";
        options.DisplayRequestDuration();
    });

    app.MapGet("/", () => Results.Redirect("/swagger"))
        .ExcludeFromDescription();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = registration => registration.Tags.Contains(LivenessTag),
    ResponseWriter = HealthCheckResponseWriter.WriteAsync,
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = registration => registration.Tags.Contains(ReadinessTag),
    ResponseWriter = HealthCheckResponseWriter.WriteAsync,
});

app.MapControllers();

app.Run();

// WebApplicationFactory uses this partial type as the integration-test entry point.
public partial class Program;
