# ADR 0001: Use a Layered Modular Monolith

- **Status:** Accepted
- **Date:** 2026-08-08

## Context

RangeOps contains meaningful business policy, HTTP endpoints, authentication,
PostgreSQL persistence, and automated tests. Putting all of those concerns in a
single API project would be quick initially but would make domain rules harder to
test and easier to couple to framework details.

Splitting the system into independently deployed microservices would add network
failure modes, distributed transactions, more container orchestration, and
operational cost without a demonstrated scaling or team-ownership need.

## Decision

Build one deployable API and one database while separating code into projects:

```text
RangeOps.Api
  -> RangeOps.Application
  -> RangeOps.Infrastructure

RangeOps.Infrastructure
  -> RangeOps.Application
  -> RangeOps.Domain

RangeOps.Application
  -> RangeOps.Domain

RangeOps.Domain
  -> no project dependencies
```

The compiler-enforced reference direction is part of the architecture. Runtime
dependency injection will connect application interfaces to infrastructure
implementations at the API composition root.

## Consequences

### Benefits

- Domain rules can be unit-tested without HTTP, EF Core, or Docker.
- Persistence can change without rewriting the domain model.
- The API layer stays focused on HTTP contracts and status codes.
- One deployable service keeps development and operations manageable.
- Project references make accidental dependency violations visible at build
  time.

### Costs

- More projects and mapping code than a single-project CRUD API.
- Developers must decide which layer owns each type.
- Interfaces can become unnecessary ceremony if introduced without a real
  boundary.

## Guardrails

- Do not create an interface for every class automatically.
- Domain types must not reference ASP.NET Core, Entity Framework Core, or Npgsql.
- Controllers must not contain readiness rules or direct database queries.
- Add a new deployable service only when an independently scalable or owned
  capability justifies it.

