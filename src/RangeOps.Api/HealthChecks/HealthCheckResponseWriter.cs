using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RangeOps.Api.HealthChecks;

internal static class HealthCheckResponseWriter
{
    public static Task WriteAsync(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = report.Status.ToString(),
            totalDurationMilliseconds = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries
                .OrderBy(entry => entry.Key, StringComparer.Ordinal)
                .Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    durationMilliseconds = entry.Value.Duration.TotalMilliseconds,
                    entry.Value.Description,
                }),
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
