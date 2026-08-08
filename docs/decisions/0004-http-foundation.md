# ADR 0004: Standardize HTTP Diagnostics and API Discovery

- **Status:** Accepted
- **Date:** 2026-08-08

## Context

RangeOps needs a consistent developer interface and machine-readable operational
signals before feature endpoints are added. Without shared HTTP conventions,
each controller could return a different error shape, deployment tooling could
not distinguish a live process from an instance ready for traffic, and API
consumers would lack interactive documentation.

## Decision

Use ASP.NET Core's first-party OpenAPI generator as the source of the API
document and Swashbuckle's Swagger UI package only as its interactive viewer.
Expose both only in the Development environment.

Use ASP.NET Core Problem Details for unhandled exceptions and otherwise empty
HTTP error responses. Include the request path and trace identifier in every
generated problem response.

Expose two unauthenticated operational endpoints:

- `GET /health/live` checks only that the API process can respond.
- `GET /health/ready` verifies that EF Core can connect to PostgreSQL.

Return a small JSON health document without exception details or connection
information.

## Consequences

### Benefits

- All future endpoints share an RFC-aligned error contract.
- Trace identifiers connect client-visible errors to server logs.
- Swagger UI remains backed by the same OpenAPI document used by tooling.
- Deployment systems can remove an unready instance from traffic without
  treating a dependency outage as a crashed process.
- Health responses reveal status and timing without exposing secrets.

### Costs

- Swagger UI adds one development-time third-party dependency.
- Database readiness performs a real connection attempt and must not be polled
  excessively.
- Detailed domain errors still need explicit mapping in later stories.

## Guardrails

- Do not expose exception messages, stack traces, or connection strings in
  Problem Details or health responses.
- Keep OpenAPI and Swagger UI disabled outside Development until production
  access requirements are deliberately defined.
- Keep liveness independent of PostgreSQL and other external dependencies.
- Use stable error type identifiers when domain-specific errors are introduced.
