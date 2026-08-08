# Definition of Done

A backlog item is done only when every applicable condition below is satisfied.

## Behavior

- Acceptance criteria are implemented and demonstrated.
- Business behavior matches the current product and architecture documents.
- Expected success, validation failure, authorization failure, missing-resource,
  conflict, and concurrency behavior are handled where applicable.
- No unrelated behavior is added to the story.

## Code quality

- Responsibilities remain in the correct architectural layer.
- Public names express domain meaning and avoid unexplained abbreviations.
- Formatting and compiler analysis complete without warnings introduced by the
  change.
- No secrets, credentials, generated build output, or local-only settings are
  committed.
- Dependencies are necessary, maintained, and pinned consistently.

## Tests

- New or changed business rules have focused unit tests.
- Critical HTTP and persistence paths have integration tests against PostgreSQL.
- Tests cover both success and meaningful failure cases.
- The complete automated test suite passes locally.
- Tests are deterministic and do not depend on execution order.

## API and data

- Endpoints have accurate OpenAPI summaries, response types, and authorization
  requirements.
- Errors use the shared Problem Details format and stable error codes.
- Database changes include reviewed EF Core migrations and constraints.
- Mutable resources apply the agreed optimistic-concurrency behavior.
- Important state-changing actions create audit records.

## Documentation and delivery

- Relevant setup, architecture, or API documentation is updated in the same
  change.
- The branch is current with `main` and contains focused, understandable commits.
- CI build and test checks pass.
- The story can be exercised from Swagger or an automated test.
- Acceptance criteria are checked before the story is closed.

## Sprint completion

A sprint is complete when:

- Every committed story is done or explicitly returned to the backlog.
- The increment builds and its full test suite passes.
- The sprint outcome is demonstrated through Swagger, tests, or documentation.
- New technical debt or follow-up work is captured in the backlog.
- A brief retrospective records what to keep, change, and try next.

## Git working agreement

- `main` stays releasable.
- Work uses short-lived branches named `feature/<story-id>-description`,
  `fix/<story-id>-description`, or `docs/<story-id>-description`.
- Commits use concise conventional prefixes such as `feat:`, `fix:`, `test:`,
  `docs:`, `refactor:`, and `chore:`.
- A story and its tests are committed together when practical.
- Pushing, merging, and releasing are deliberate user-approved actions.

