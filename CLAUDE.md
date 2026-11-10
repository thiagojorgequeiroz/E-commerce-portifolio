# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

Portfolio e-commerce project. Currently it contains a single service, `CatalogApi/` (.NET 10, ASP.NET Core), which exposes REST endpoints for products and gRPC endpoints for inventory/stock, backed by PostgreSQL via EF Core. There are no test projects yet.

## Commands

Run from `CatalogApi/` (solution file is `CatalogApi.slnx`):

```bash
dotnet build CatalogApi.slnx
dotnet run --project CatalogWebApi --launch-profile https   # https://localhost:7245, http://localhost:5202

# EF Core migrations (DbContext lives in Catalog.Database, startup project is CatalogWebApi)
dotnet ef migrations add <Name> --project Catalog.Database --startup-project CatalogWebApi
dotnet ef database update --project Catalog.Database --startup-project CatalogWebApi
```

Infrastructure (from repo root):

```bash
docker compose up -d --build                  # postgres:17 + catalog-api
docker compose -f Docker-database/docker-compose.yml up -d   # postgres only, for running the API locally
```

In compose, the API serves REST + Swagger on `http://localhost:8080` and gRPC on `localhost:8081` (HTTP/2 cleartext, configured via `Kestrel__Endpoints__*` env vars, because gRPC without TLS needs an HTTP/2-only endpoint). Test gRPC with e.g. `grpcurl -plaintext localhost:8081 list` (reflection is on in Development).

`Docker-database/init-databases.sql` creates `catalogdb` (plus future `orderdb`, `offerdb`, `notificationdb`) and runs only when the postgres volume is first created. The postgres healthcheck uses `pg_isready -h 127.0.0.1` on purpose: during first-run init, Postgres briefly listens only on its Unix socket, and a socket-based check would report healthy too early.

Migrations are applied at startup when `Database:ApplyMigrationsOnStartup` is true (set in compose and `appsettings.Development.json`, off otherwise; see `DatabaseExtensions.ApplyMigrations`). EF Core refuses to `Migrate()` when the model has pending changes, so add a migration after every entity/configuration change or the container will crash on start.

Local connection string (`appsettings*.json`, section `Database:ConnectionString`) points to `localhost:5432/catalogdb` with `postgres/postgres`; in compose it is overridden via `Database__ConnectionString`.

## Architecture

Clean-architecture / CQRS layout with MediatR. Project folder names differ from assembly/namespace names:

| Folder | Assembly / root namespace | Role |
|---|---|---|
| `CatalogApi.Domain` | `Catalog.Domain` | Entities, domain exceptions, repository interfaces (folder is spelled `Respositories`) |
| `Catalog.Contract` | `Catalog.Application.Contract` | MediatR request/response DTOs **and their FluentValidation validators in the same file** |
| `Catalog.Command` | `Catalog.Application.Command` | Command handlers — write through domain repositories + `IUnitOfWork` |
| `Catalog.Query` | `Catalog.Application.Query` | Query handlers — read directly from `CatalogDbContext` with `AsNoTracking` projections (no repositories) |
| `Catalog.Application` | `Catalog.Application` | `AddApplication()` DI wiring, MediatR pipeline behaviors (Logging, Validation) |
| `Catalog.Database` | `Catalog.Database` | `CatalogDbContext`, `IEntityTypeConfiguration` classes, repositories, `UnitOfWork`, migrations, `AddDatabase()` |
| `CatalogWebApi` | `CatalogWebApi` | Host: versioned controllers, gRPC services, Swagger, global exception handler |

Contracts, commands and queries are organized by transport: `v1/...` for REST, `Grpc/...` for gRPC. Each handler-bearing assembly has an `*AssemblyReference` marker class used for MediatR/validator assembly scanning — new handlers/validators in those projects are picked up automatically.

### Request flow
Controller or gRPC service → `IMediator.Send` → `LoggingBehavior` → `ValidationBehavior` (throws `ValidationAppException` on failures) → handler. Controllers and gRPC services contain no logic beyond mapping to MediatR requests. Request DTO properties are nullable (`Guid?`, `int?`) so validators can report missing values; handlers then use `!.Value`.

### Error handling
Domain errors derive from `AppException` (carries `StatusCode` and `Title`): `BadRequestException`, `NotFoundException`, `DatabaseException`, `DomainException`, etc. `GlobalExceptionHandler` maps them to `ProblemDetails` (validation errors to `ValidationProblemDetails`); anything else becomes a 500 `UnexpectedException`. Repositories throw `NotFoundException` rather than returning null; `UnitOfWork` wraps `PostgresException` into `DatabaseException`. Note this handler applies to HTTP; gRPC calls rely on `EnableDetailedErrors`.

### Domain model
Entities inherit `EntityBase` (Id, CreatedAt, UpdatedAt, `UpdateTimestamp()`), use private setters, and enforce invariants in methods (e.g. `Inventory.ReserveStock/ReleaseStock`). `Product` creates its own `Inventory` in its constructor; the relationship is 1:1 with **`Inventory.Id` being both PK and FK to `Product.Id`**, so inventory is looked up by product id.

### gRPC
`CatalogWebApi/Grpc/Inventory/InventoryService.proto` (compiled with `GrpcServices="Both"`) defines `CheckStock`, `ReserveStock`, `ReleaseStock`, implemented by `InventoryGrpcService`. gRPC reflection is enabled in Development.

### API versioning
Asp.Versioning with URL segment routes: `v{version:apiVersion}/[controller]`; Swagger documents are generated per version.
