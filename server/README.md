# Server Backend Architecture

This directory contains the current ASP.NET Core Web API backend for Tabo-Ani.

The backend uses a layered structure inside `server/TaboAni.Api`:

- `Api`: HTTP entry points
- `Application`: contracts, DTOs, mapping, and app-level wiring
- `Domain`: entity model
- `Data`: EF Core `DbContext`, entity configurations, and migrations
- `Infrastructure`: repository, service, and unit-of-work implementations
- `Verification`: schema verification utility

## Current Architecture

The backend is organized as a pragmatic layered architecture, not a fully isolated clean architecture. The layers are separated by folder and interface boundaries, but they still live in one project.

### Request flow

The only fully wired vertical slice today is `Orders`:

1. `OrdersController` receives HTTP requests and returns consistent success/error JSON envelopes.
2. `IOrderService` defines the application service contract.
3. `OrderService` handles orchestration and transaction boundaries through `IUnitOfWork`.
4. `IOrderRepository` defines persistence operations for orders.
5. `OrderRepository` uses `AppDbContext` and EF Core to query or persist data.
6. `AppDbContext` maps `Domain` entities to PostgreSQL tables using fluent configurations.

In practice, the runtime chain is:

`HTTP -> Controller -> Service -> UnitOfWork/Repository -> AppDbContext -> PostgreSQL`

### Composition root

`Program.cs` is the composition root. It is responsible for:

- loading `.env` values before app startup
- resolving `ConnectionStrings__DefaultConnection`
- registering controllers
- registering OpenAPI and Scalar API docs in development
- registering application dependencies
- configuring EF Core with Npgsql
- enabling CORS for `http://localhost:5173`

### Dependency injection

Dependency registration is centralized in `Application/Extensions/DependencyInjectionExtension.cs`.

Current registrations:

- `IOrderRepository -> OrderRepository`
- `IUnitOfWork -> UnitOfWork`
- `IOrderService -> OrderService`

Mapster configuration is also scanned there so DTO/entity mappings are available application-wide.

### Data model layer

The domain model is broad and already covers most platform concepts:

- users and roles
- KYC
- produce listings and inventory
- carts and orders
- wallets, payments, and escrow
- deliveries and QA
- conversations and messages
- reviews and audit logs

That broad model is represented in:

- `Domain/Entities/*.cs`
- `Data/Configurations/*.cs`
- `Data/AppDbContext.cs`
- `Data/Migrations/*.cs`

Important detail: the schema footprint is much larger than the exposed API surface. The schema is modeled for many modules, but the controller/service/repository pipeline is currently implemented only for `Orders`.

### Transaction handling

Transaction coordination is implemented through `IUnitOfWork` and `UnitOfWork`.

Current behavior:

- starts explicit EF Core database transactions
- commits after `SaveChangesAsync`
- rolls back on failure
- clears the EF change tracker after rollback

Right now, `UnitOfWork` exposes only `Orders`, so it is currently a narrow unit-of-work implementation rather than a repository aggregator for all modules.

### Mapping and DTOs

The backend uses Mapster for DTO mapping.

Current DTO coverage is also centered on orders:

- request DTO: `OrderRequestDto`
- response DTOs: `OrderResponseDto`, `ApiResponseDto<T>`, `ErrorResponseDto`

Mapping is configured in:

- `Application/Configuration/MapsterConfiguration/OrderMapsterConfiguration.cs`
- `Application/Extensions/MappingExtensions/OrderMappingExtensions.cs`

### API documentation

Development-time API docs are enabled through OpenAPI and Scalar:

- `AddOpenApi()`
- `MapOpenApi()`
- `MapScalarApiReference()`

This is defined in `Application/Extensions/ApiDocumentationExtensions.cs`.

### Environment and configuration

The backend loads `.env` manually using `Application/Configuration/DotEnv.cs`.

The required setting is:

```env
ConnectionStrings__DefaultConnection=Host=...;Port=5432;Database=...;Username=...;Password=...;SSL Mode=Require;Trust Server Certificate=true
```

This value is then read through standard configuration as `ConnectionStrings:DefaultConnection`.

### Verification utility

`Verification/SchemaVerificationRunner.cs` is a small architecture guardrail for the EF Core model. It verifies:

- expected table names
- selected indexes and filters
- important constraints in the generated create script

It can be triggered with:

```powershell
dotnet run -- --verify-schema
```

## Current File Structure

```text
server/
|-- README.md
`-- TaboAni.Api/
    |-- Api/
    |   `-- Controllers/
    |       `-- OrdersController.cs
    |-- Application/
    |   |-- Configuration/
    |   |   |-- DotEnv.cs
    |   |   `-- MapsterConfiguration/
    |   |       `-- OrderMapsterConfiguration.cs
    |   |-- DTOs/
    |   |   |-- Request/
    |   |   |   `-- OrderRequestDto.cs
    |   |   `-- Response/
    |   |       |-- ApiResponseDto.cs
    |   |       |-- ErrorResponseDto.cs
    |   |       `-- OrderResponseDto.cs
    |   |-- Extensions/
    |   |   |-- ApiDocumentationExtensions.cs
    |   |   |-- DependencyInjectionExtension.cs
    |   |   `-- MappingExtensions/
    |   |       `-- OrderMappingExtensions.cs
    |   `-- Interfaces/
    |       |-- Repository/
    |       |   |-- IOrderRepository.cs
    |       |   `-- IUnitOfWork.cs
    |       `-- Service/
    |           `-- IOrderService.cs
    |-- Data/
    |   |-- AppDbContext.cs
    |   |-- Configurations/
    |   |   |-- EntityTypeBuilderExtensions.cs
    |   |   |-- UserConfiguration.cs
    |   |   |-- ProduceListingConfiguration.cs
    |   |   |-- OrderConfiguration.cs
    |   |   |-- PaymentConfiguration.cs
    |   |   |-- DeliveryConfiguration.cs
    |   |   |-- QaReportConfiguration.cs
    |   |   `-- ... many other entity configurations
    |   `-- Migrations/
    |       `-- ... EF Core migrations and snapshot
    |-- Domain/
    |   `-- Entities/
    |       |-- User.cs
    |       |-- ProduceListing.cs
    |       |-- Order.cs
    |       |-- Payment.cs
    |       |-- Delivery.cs
    |       |-- QaReport.cs
    |       `-- ... many other entities
    |-- Infrastructure/
    |   `-- Implementations/
    |       |-- UnitOfWork.cs
    |       |-- Repository/
    |       |   `-- OrderRepository.cs
    |       `-- Service/
    |           `-- OrderService.cs
    |-- Properties/
    |   `-- launchSettings.json
    |-- Verification/
    |   `-- SchemaVerificationRunner.cs
    |-- .env
    |-- .env.example
    `-- Program.cs
```

## Practical Reading Guide

If you want to understand the backend quickly, read the files in this order:

1. `Program.cs`
2. `Application/Extensions/DependencyInjectionExtension.cs`
3. `Api/Controllers/OrdersController.cs`
4. `Application/Interfaces/Service/IOrderService.cs`
5. `Infrastructure/Implementations/Service/OrderService.cs`
6. `Application/Interfaces/Repository/IUnitOfWork.cs`
7. `Infrastructure/Implementations/UnitOfWork.cs`
8. `Application/Interfaces/Repository/IOrderRepository.cs`
9. `Infrastructure/Implementations/Repository/OrderRepository.cs`
10. `Data/AppDbContext.cs`

## Architectural Assessment

The current backend is in a good transitional state for a modular CRUD API, but it is not yet complete across modules.

What is already in place:

- clear layer boundaries by folder and interface
- thin controller for the `Orders` slice
- EF Core model and migrations for a wide business domain
- explicit transaction handling
- centralized DI and mapping registration
- consistent response envelopes

What is still partial:

- only one controller exists
- only one service contract exists
- only one repository is exposed through the unit of work
- most domain modules are modeled in EF Core but not yet surfaced through application services and APIs

The practical conclusion is:

The project already has the skeleton for a scalable service/repository-based backend, but the implementation depth is currently concentrated in the `Orders` module while the rest of the platform is mostly represented at the schema level.
