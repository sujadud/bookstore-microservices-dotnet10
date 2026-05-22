# Setup / Quickstart

This guide explains how to run the bookstore microservices locally for development and testing.

Prerequisites

- .NET 10 SDK (install from https://dotnet.microsoft.com)
- Docker Desktop (with Compose support)
- Git (to clone the repository)
- Optional: Postman or HTTP client, Redis CLI, MongoDB tools

Run with Docker Compose (recommended)

1. From the repository root:

   docker-compose up --build

2. Wait until containers report healthy. Important containers include RabbitMQ, MongoDB, Redis, Postgres/SQL Server and each microservice.

Run individual services (developer workflow)

1. Open the solution `src\BookStore.Microservice.DotNet10.sln` in your IDE (JetBrains Rider, Visual Studio, VS Code)
2. Set the startup project to the service you want to run (for instance `Catalog.API`) and run it. Services expose ports as configured in their `Properties\launchSettings.json` or `appsettings.json`.

Database initialization and seeding

- Some services include seeders: e.g., `CatalogDbContextSeed` (Catalog service) and `DbInitializer` (Discount API). When running with Docker Compose, seeders will run if configured in the Dockerfiles or container entrypoints.

Ports and hostnames

- The API Gateway (Ocelot) typically exposes a gateway port (see `src\ApiGateways\OcelotApiGw\Properties\launchSettings.json` and `ocelot.json`).
- Individual services expose their ports; consult service `appsettings.json` files.

Environment variables

The solution uses environment variables for connection strings and settings. When using Docker Compose, these are set in `docker-compose.yml`. For local development, set them in your IDE run profile or use a `.env` file.

Rebuilding a single service image

Docker build -f src/Services/Catalog.API/Dockerfile -t bookstore/catalog.api:local .

Cleanup

Docker-compose down -v

Notes

- Keep Docker Desktop memory high enough if running multiple databases/containers.
- If you need to reset DB state, remove volumes with `docker-compose down -v` and restart.

