# RangeOps — Mission Readiness API

RangeOps is a defense-style backend system for tracking equipment readiness,
maintenance work, mission requirements, and mission approval decisions.

The central capability is a readiness engine that evaluates every asset assigned
to a mission and reports the exact issues that prevent approval.

## Project status

**Current phase:** Sprint 1 — Walking skeleton

The solution boundaries and engineering defaults are in place. No RangeOps
business features have been implemented yet.

## Planned technology stack

- C# and .NET 10 LTS
- ASP.NET Core Web API
- Entity Framework Core and PostgreSQL
- Swagger UI / OpenAPI
- Docker and Docker Compose
- xUnit, FluentAssertions, and Testcontainers
- GitHub Actions

## Core workflow

1. An operator registers an asset and records its readiness state.
2. A maintainer opens, assigns, resolves, and verifies maintenance tickets.
3. An operator creates a mission window and assigns the required assets.
4. The readiness engine checks status, inspections, critical tickets, and
   scheduling conflicts.
5. An authorized approver may approve the mission only when no blockers remain.
6. Readiness changes and important actions are retained for traceability.

## Planning documents

- [Product brief](docs/product-brief.md)
- [Architecture](docs/architecture.md)
- [Product backlog](docs/product-backlog.md)
- [Definition of done](docs/definition-of-done.md)
- [Sprint 0 plan](docs/sprints/sprint-00-foundation.md)
- [Sprint 1 plan](docs/sprints/sprint-01-walking-skeleton.md)
- [Architecture decision records](docs/decisions/)

## Planned delivery sequence

| Sprint | Outcome |
| --- | --- |
| 0 | Product scope, architecture, backlog, and working agreements |
| 1 | Running API, PostgreSQL persistence, Swagger, and test harness |
| 2 | Authentication, authorization foundation, and asset management |
| 3 | Readiness history and maintenance workflow |
| 4 | Missions, asset assignments, and scheduling rules |
| 5 | Readiness validation and authorized mission approval |
| 6 | Audit logging, concurrency, filtering, and pagination hardening |
| 7 | Full integration coverage, containers, CI, and portfolio documentation |
