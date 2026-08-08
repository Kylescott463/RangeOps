using RangeOps.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var databaseConnectionString = builder.Configuration.GetConnectionString("RangeOps")
    ?? throw new InvalidOperationException(
        "Connection string 'RangeOps' is not configured. See docs/setup/local-development.md.");

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(databaseConnectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// WebApplicationFactory uses this partial type as the integration-test entry point.
public partial class Program;
