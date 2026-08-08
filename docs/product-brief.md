# Product Brief

## Product vision

RangeOps gives test-range personnel one reliable answer to a high-value
question: **Is this mission ready to execute, and if not, what is blocking it?**

It replaces readiness decisions based on scattered notes or assumptions with a
repeatable, auditable decision based on current asset, inspection, maintenance,
and scheduling data.

## Primary users

| Role | Responsibilities |
| --- | --- |
| Operator | Manage assets, readiness updates, missions, and asset assignments |
| Maintainer | Open, accept, resolve, and verify maintenance work |
| Mission Approver | Validate and approve missions that have no blockers |
| Administrator | Manage users and perform all authorized operations |

One person may hold more than one role. The API will enforce permissions rather
than relying on the client interface.

## MVP capabilities

The first release must support:

- Registering and viewing assets
- Recording readiness changes with a mandatory reason
- Viewing asset readiness history
- Opening, assigning, resolving, and verifying maintenance tickets
- Creating missions with scheduled start and end times
- Assigning specific required assets to missions
- Detecting overlapping mission assignments
- Producing a deterministic mission-readiness report
- Preventing approval while any blocking issue exists
- Restricting mission approval to authorized users
- Recording important actions in an audit log
- Protecting simultaneous updates with optimistic concurrency
- Filtering and paginating collection endpoints
- Returning consistent Problem Details error responses

## Readiness policy

A mission is ready only when all of the following are true:

1. It has at least one required asset.
2. Every required asset is in `Operational` status.
3. Every required asset's next inspection date is on or after the mission start
   date.
4. No required asset has an open critical maintenance ticket.
5. No required asset is assigned to another overlapping active mission.

Validation returns all known blockers in one response. It does not stop after the
first failure. Running validation never approves a mission or changes asset
readiness.

## Workflow decisions

### Asset readiness

Initial statuses are:

- `Operational`
- `UnderMaintenance`
- `Unavailable`

Every readiness update requires a nonblank reason and creates both a readiness
event and an audit entry. Invalid transitions will be rejected by domain rules.

### Maintenance tickets

Initial severities are `Low`, `Medium`, `High`, and `Critical`.

The lifecycle is:

```text
Open -> Assigned -> ResolvedPendingVerification -> Verified
```

A resolution must include resolution notes. A ticket does not become verified
until a different authorized user verifies the work. Open critical tickets block
readiness; verified tickets do not.

### Missions

Initial statuses are:

```text
Draft -> Approved -> Completed
   |         |
   +------> Cancelled
```

Validation is an evaluation, not a persisted mission status. Approval always
runs fresh validation inside the approval operation so a stale validation result
cannot be used.

## Out of scope for the MVP

- A custom graphical frontend
- Real-time telemetry or hardware integration
- Classified information or real operational data
- Notifications, email, or SMS
- Inventory purchasing and financial workflows
- Multi-organization tenancy
- File attachments
- Cloud deployment

These can be considered only after the MVP is complete.

## Product success criteria

The MVP is successful when:

- All documented business rules have automated unit tests.
- Critical API workflows pass against a real temporary PostgreSQL container.
- `docker compose up --build` starts the API and database.
- An authenticated user can exercise the complete workflow through Swagger UI.
- A conflicting approval returns HTTP `409 Conflict` with every current blocker.
- The main branch build and test workflow passes in GitHub Actions.
- The README contains verified setup instructions, diagrams, examples, and test
  evidence.

