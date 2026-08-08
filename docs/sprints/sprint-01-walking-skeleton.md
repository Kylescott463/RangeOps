# Sprint 1 — Walking Skeleton

## Sprint details

- **Duration:** One week
- **Developer:** Solo
- **Status:** In progress
- **Sprint goal:** Produce the smallest end-to-end technical foundation that can
  build, start, describe itself through OpenAPI, connect to PostgreSQL, and prove
  its behavior through automated tests.

## Why this sprint comes first

A walking skeleton proves the risky connections early without pretending the
product is finished. Before adding assets or missions, we want evidence that the
solution structure, API host, database provider, container workflow, and test
strategy work together.

This avoids discovering foundational problems after business code has already
been built on top of them.

## Committed stories

| ID | Story | Demonstrable outcome |
| --- | --- | --- |
| FND-004 | Create the solution and project boundaries | The full solution builds and its references enforce the planned dependency direction |
| FND-005 | Add PostgreSQL persistence foundation | The API can connect to a Compose-managed PostgreSQL database |
| FND-006 | Add the HTTP foundation | Health and OpenAPI endpoints run with consistent Problem Details configuration |
| FND-007 | Establish automated test harnesses | Unit and integration test projects execute successfully |
| FND-008 | Add initial CI | GitHub Actions restores, builds, and tests the solution |

## Delivery order

1. **Solution structure:** Create projects and dependency references.
2. **Engineering defaults:** Add SDK pinning, code-style rules, and centralized
   package versions.
3. **Persistence:** Add EF Core, Npgsql, an empty application `DbContext`, and
   PostgreSQL configuration.
4. **HTTP host:** Add health, OpenAPI/Swagger UI, and Problem Details.
5. **Containers:** Add API and database Docker definitions plus health checks.
6. **Testing:** Add one architecture/unit test and one real API/PostgreSQL
   integration smoke test.
7. **Automation:** Reproduce restore, build, and test in GitHub Actions.

Each step must build before the next begins.

## Learning objectives

By the end of this sprint, the developer should be able to explain:

- The difference between a solution and a project in .NET.
- Why compile-time project references enforce architectural boundaries.
- Why domain code must not depend on HTTP or database frameworks.
- How dependency injection connects interfaces to infrastructure at runtime.
- The roles of the SDK, NuGet restore, compiler, application host, and runtime.
- Why production code uses configuration instead of hard-coded connection
  strings.
- The difference between a unit test and an integration test.
- Why a container image, running container, Compose service, and volume are
  different concepts.
- How continuous integration protects the main branch.

## Acceptance criteria

- The repository uses `src/` for production projects and `tests/` for tests.
- Domain has no project or framework dependencies.
- Application references only Domain.
- Infrastructure references Application and Domain.
- API references Application and Infrastructure.
- Unit tests reference Domain and Application.
- Integration tests reference the API host.
- The solution restores, builds with warnings treated as errors, and passes all
  tests.
- Docker Compose starts a healthy API and PostgreSQL service.
- Swagger UI and a health endpoint are reachable locally.
- GitHub Actions repeats restore, build, and tests successfully.
- No secrets or generated output are committed.

## Sprint tasks

- [x] Create a short-lived feature branch for FND-004.
- [x] Record the key architecture decisions and their tradeoffs.
- [x] Scaffold the solution and six projects.
- [x] Configure project references.
- [x] Remove sample template behavior.
- [x] Add repository-wide SDK and code-quality defaults.
- [x] Restore and build the complete solution.
- [x] Run the initial unit and integration test projects.
- [x] Review and commit FND-004.
- [ ] Complete FND-005 through FND-008 one story at a time.
- [ ] Review the sprint increment with the project owner.
- [ ] Complete the sprint retrospective.

## Retrospective

Complete at sprint close:

- **Keep:** To be recorded
- **Change:** To be recorded
- **Try next:** To be recorded
