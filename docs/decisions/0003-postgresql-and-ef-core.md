# ADR 0003: Use PostgreSQL with EF Core Code-First Migrations

- **Status:** Accepted
- **Date:** 2026-08-08

## Context

RangeOps needs durable relational data for assets, missions, assignments,
maintenance tickets, readiness history, audit logs, and users. Its central rules
depend on relationships and consistency: an assigned asset must exist, mission
assignments must be unique, and approval must observe a consistent database
state.

Hand-written SQL offers maximum control but would require us to write connection
management, materialization, change tracking, and transaction plumbing before
delivering business behavior. A document database would make the relationship
and constraint-heavy model harder to enforce.

## Decision

Use PostgreSQL as the relational database and Entity Framework Core with the
Npgsql provider as the persistence technology. Define the model in code and
store generated migrations in `RangeOps.Infrastructure`.

The API is the composition root: it reads the connection string from external
configuration and passes it into Infrastructure. `RangeOpsDbContext` is scoped
to one request by ASP.NET Core dependency injection.

Do not add a generic repository abstraction. EF Core already provides repository
and unit-of-work behavior. Add application-facing interfaces only when a use
case needs a persistence boundary that improves business-focused testing or
prevents infrastructure details from leaking upward.

## Consequences

### Benefits

- PostgreSQL provides transactions, foreign keys, uniqueness constraints, and
  concurrency features that match the problem.
- EF Core reduces routine data-access code and creates repeatable schema
  migrations.
- The domain remains independent of the database provider.
- A scoped context gives each HTTP request an isolated change-tracking session.
- The real PostgreSQL provider can be exercised in integration tests rather than
  relying on behaviorally different in-memory substitutes.

### Costs

- Developers must understand generated SQL, tracking, transactions, and query
  performance instead of treating the ORM as magic.
- Schema changes require reviewed migrations.
- The API needs valid database configuration before it can start.
- `DbContext` is not thread-safe and must not be shared across concurrent work.

## Guardrails

- Never commit passwords or production connection strings.
- Keep EF Core types and entity configurations in Infrastructure.
- Keep readiness policy in Domain/Application rather than entity configurations
  or controllers.
- Pin compatible major versions of EF Core and Npgsql.
- Use PostgreSQL Testcontainers for integration tests.
- Review generated migrations and SQL before applying them.
