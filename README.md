# Bookstore Microservices with .NET 10

TL;DR

A cloud-ready, event-driven e-commerce reference implementation built with .NET 10. The repository demonstrates a small microservices ecosystem with an API Gateway, messaging, polyglot persistence, and container-first tooling for local development and CI.

Highlights

- Microservices: Catalog (Books), Basket, Discount (API + gRPC), Ordering
- RabbitMQ for async messaging and events
- Polyglot persistence: MongoDB, Redis, PostgreSQL, SQL Server
- Ocelot API Gateway for routing and aggregation
- Developer-friendly docs, Swagger, `.http` examples, and Dockerfiles

Quickstart

1. Read the setup guide: `documents/SETUP.md`
2. Start the stack with Docker Compose: `docker-compose up --build`
3. Explore APIs via Swagger and the `.http` examples in `src/*/*.http`

Documentation

All supporting documentation lives under `documents/`:

- `documents/SETUP.md` — local setup and Docker Compose
- `documents/USAGE.md` — example requests and gRPC guidance
- `documents/ARCHITECTURE.md` — system overview and data flow
- `documents/CONTRIBUTING.md` — contribution workflow and PR guidance
- `documents/API-REFERENCE.md` — compact endpoint reference
- `documents/DOCKER.md` — container and compose notes
- `documents/TROUBLESHOOTING.md` — common issues and fixes
- `documents/LICENSE-NOTES.md` — licensing and contributor notes

Repository layout

- `src/` — solution and services
- `documents/` — all non-README documentation and diagram assets
- `docker-compose.yml` — local orchestration
- `LICENSE` — project license (MIT)

License

This project is released under the MIT License. See `LICENSE` for details.
