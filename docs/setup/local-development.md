# Local Development Setup

This guide starts the PostgreSQL dependency and configures the API without
committing a password.

## 1. Create local Compose settings

From the repository root:

```bash
cp .env.example .env
```

Open `.env` and replace the example password. Docker Compose reads this file,
but Git ignores it. `.env.example` documents the required variable names and is
safe to commit because it contains no real secret.

## 2. Start PostgreSQL

```bash
docker compose up -d postgres
docker compose ps
```

Compose creates:

- an image: the packaged PostgreSQL software;
- a container: the running PostgreSQL process;
- a service: Compose's configuration for that container;
- a named volume: durable database files that survive container replacement.

Wait until `docker compose ps` reports the service as healthy.

## 3. Configure the API connection string

Use the same database name, username, password, and port from `.env`:

```bash
dotnet user-secrets set \
  --project src/RangeOps.Api/RangeOps.Api.csproj \
  "ConnectionStrings:RangeOps" \
  "Host=localhost;Port=5432;Database=rangeops;Username=rangeops;Password=YOUR_LOCAL_PASSWORD"
```

.NET User Secrets stores this development value outside the repository. It is a
convenience for local development, not a production secret manager.

## 4. Run the API

```bash
dotnet run \
  --project src/RangeOps.Api/RangeOps.Api.csproj \
  --launch-profile https
```

The API deliberately fails during startup when the connection string is absent.
This fail-fast behavior turns a hidden configuration problem into a clear error.

Open the interactive API documentation at:

```text
https://localhost:5001/swagger
```

Operational checks are available at:

```text
https://localhost:5001/health/live
https://localhost:5001/health/ready
```

## 5. Run automated tests

Keep Docker Desktop running, then execute:

```bash
dotnet test RangeOps.sln
```

The integration test starts its own temporary PostgreSQL container on a random
host port. It does not read or modify the Compose-managed development database.
Testcontainers removes the temporary container after the test run.

## Useful commands

```bash
# Follow database logs
docker compose logs -f postgres

# Stop containers while retaining database data
docker compose down

# Stop containers and delete local database data
docker compose down --volumes
```

The final command is destructive because it removes the named database volume.
Use it only when you intentionally want a clean local database.
