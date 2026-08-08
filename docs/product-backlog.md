# Product Backlog

## Prioritization

- **P0:** Required for the MVP to function safely
- **P1:** Required for a portfolio-quality MVP
- **P2:** Valuable follow-up after the MVP

Story identifiers remain stable so commits, branches, tests, and pull requests
can refer to the same work item.

## Release backlog

| ID | Priority | Target | User story / outcome |
| --- | --- | --- | --- |
| FND-001 | P0 | Sprint 0 | Define the product scope and readiness policy |
| FND-002 | P0 | Sprint 0 | Record architecture and engineering decisions |
| FND-003 | P0 | Sprint 0 | Establish backlog, sprint plan, and definition of done |
| FND-004 | P0 | Sprint 1 | Create the .NET solution and project boundaries |
| FND-005 | P0 | Sprint 1 | Start the API and PostgreSQL through local tooling |
| FND-006 | P0 | Sprint 1 | Expose Swagger/OpenAPI, health checks, and Problem Details |
| FND-007 | P0 | Sprint 1 | Establish unit and PostgreSQL integration test harnesses |
| FND-008 | P1 | Sprint 1 | Add initial GitHub Actions build and test checks |
| SEC-001 | P0 | Sprint 2 | Authenticate API users with JWT bearer tokens |
| SEC-002 | P0 | Sprint 2 | Authorize operations with Operator, Maintainer, Approver, and Admin roles |
| AST-001 | P0 | Sprint 2 | Register an asset with a unique code |
| AST-002 | P0 | Sprint 2 | Retrieve one asset and a filtered, paginated asset collection |
| AST-003 | P0 | Sprint 2 | Change readiness only with a valid transition and reason |
| AST-004 | P0 | Sprint 3 | View an asset's chronological readiness history |
| MNT-001 | P0 | Sprint 3 | Open a maintenance ticket for an asset |
| MNT-002 | P0 | Sprint 3 | Assign an open ticket to an authorized maintainer |
| MNT-003 | P0 | Sprint 3 | Resolve a ticket with resolution notes |
| MNT-004 | P0 | Sprint 3 | Require independent verification before closing a ticket |
| MNT-005 | P0 | Sprint 3 | Make non-verified critical tickets readiness blockers |
| MSN-001 | P0 | Sprint 4 | Create and retrieve missions with valid scheduling windows |
| MSN-002 | P0 | Sprint 4 | Assign a required asset to a mission only once |
| MSN-003 | P0 | Sprint 4 | Reject overlapping assignment of the same asset |
| RDY-001 | P0 | Sprint 5 | Validate all required assets and return every blocking issue |
| RDY-002 | P0 | Sprint 5 | Block assets that are unavailable or under maintenance |
| RDY-003 | P0 | Sprint 5 | Block assets whose inspection expires before mission start |
| RDY-004 | P0 | Sprint 5 | Block assets with active critical tickets |
| APR-001 | P0 | Sprint 5 | Allow only an authorized approver to approve a ready mission |
| APR-002 | P0 | Sprint 5 | Revalidate atomically during approval and return `409` on blockers |
| AUD-001 | P1 | Sprint 6 | Audit readiness, maintenance, assignment, and approval actions |
| CON-001 | P1 | Sprint 6 | Reject stale simultaneous updates with `409 Conflict` |
| API-001 | P1 | Sprint 6 | Apply consistent filtering and pagination conventions |
| API-002 | P1 | Sprint 6 | Apply consistent validation and Problem Details responses |
| TST-001 | P0 | Every sprint | Unit-test each business rule introduced by the sprint |
| TST-002 | P0 | Every sprint | Integration-test each critical API workflow introduced by the sprint |
| OPS-001 | P1 | Sprint 7 | Produce production-style API and PostgreSQL Docker images/configuration |
| OPS-002 | P1 | Sprint 7 | Run build, unit tests, and integration tests in GitHub Actions |
| DOC-001 | P1 | Sprint 7 | Complete README setup, architecture, examples, screenshots, and test evidence |
| EXT-001 | P2 | Later | Send readiness notifications |
| EXT-002 | P2 | Later | Add a lightweight readiness dashboard |
| EXT-003 | P2 | Later | Attach maintenance evidence files |

## Initial API contract

The MVP will evolve toward these endpoint groups:

```text
POST   /api/auth/login

POST   /api/assets
GET    /api/assets
GET    /api/assets/{id}
PATCH  /api/assets/{id}/readiness
GET    /api/assets/{id}/history

POST   /api/missions
GET    /api/missions
GET    /api/missions/{id}
POST   /api/missions/{id}/assets
POST   /api/missions/{id}/validate
POST   /api/missions/{id}/approve

POST   /api/maintenance-tickets
GET    /api/maintenance-tickets
GET    /api/maintenance-tickets/{id}
PATCH  /api/maintenance-tickets/{id}/assign
PATCH  /api/maintenance-tickets/{id}/resolve
PATCH  /api/maintenance-tickets/{id}/verify
```

Endpoint details will be finalized immediately before their implementation. This
prevents premature contracts from locking in a flawed domain design.

