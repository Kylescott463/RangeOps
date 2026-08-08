# ADR 0002: Use Controller-Based HTTP Endpoints

- **Status:** Accepted
- **Date:** 2026-08-08

## Context

ASP.NET Core supports both minimal APIs and controllers. RangeOps will expose
several resource groups, role-protected operations, structured response types,
and Swagger documentation.

Minimal APIs reduce ceremony and are a strong choice for small services or a
small number of endpoints. As the endpoint count grows, route definitions and
their metadata can become crowded unless the team imposes another organization
pattern.

## Decision

Use API controllers grouped by resource: assets, missions, maintenance tickets,
authentication, and later audit queries.

Controllers will translate HTTP requests into application use cases and convert
results into HTTP responses. They will not implement business rules.

## Consequences

### Benefits

- Familiar resource-based organization for a junior developer and reviewers.
- Clear locations for authorization, routing, and response metadata.
- Strong support for model binding, filters, testing, and OpenAPI generation.
- Endpoint groups remain readable as the API expands.

### Costs

- More attributes and boilerplate than minimal APIs.
- Controllers can become oversized if orchestration leaks into them.

## Guardrails

- Keep controller actions small.
- Use request/response contracts rather than exposing persistence entities.
- Put validation and business policy in the appropriate application or domain
  layer.
- Return standardized Problem Details for failures.

