using System.Reflection;
using FluentAssertions;

namespace RangeOps.UnitTests.Architecture;

public sealed class DomainDependencyTests
{
    [Fact]
    public void DomainAssemblyDoesNotReferenceOuterLayers()
    {
        string[] forbiddenPrefixes =
        [
            "RangeOps.Api",
            "RangeOps.Application",
            "RangeOps.Infrastructure",
            "Microsoft.AspNetCore",
            "Microsoft.EntityFrameworkCore",
            "Npgsql",
        ];

        var forbiddenReferences = Assembly
            .Load("RangeOps.Domain")
            .GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .Where(name => name is not null)
            .Cast<string>()
            .Where(name => forbiddenPrefixes.Any(prefix =>
                name.StartsWith(prefix, StringComparison.Ordinal)));

        forbiddenReferences.Should().BeEmpty();
    }
}
