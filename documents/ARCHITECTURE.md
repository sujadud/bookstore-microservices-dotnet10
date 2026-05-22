# Architecture Overview

This document summarizes the high-level architecture of the bookstore microservices reference implementation.

Components

- API Gateway (Ocelot) — single-entry point for clients, routing to backend services and aggregating responses.
- Catalog Service — stores book data (MongoDB)
- Basket Service — short-lived shopping cart state stored in Redis
- Discount Service — coupon management (also exposes gRPC for discount lookup)
- Ordering Service — processes orders and persists in SQL/Relational store
- Event Bus (RabbitMQ) — asynchronous communication for events (e.g., BasketCheckoutEvent)
- Datastores — MongoDB, Redis, PostgreSQL, SQL Server depending on service

Data flow (example)

1. Client → API Gateway → Catalog Service to list books
2. Client adds items to Basket Service (Redis)
3. On checkout, Basket Service publishes a `BasketCheckoutEvent` to RabbitMQ
4. Ordering Service consumes the event, creates an order and persists it to the relational DB

Key files to reference

- Solution: `src\BookStore.Microservice.DotNet10.sln`
- Gateway config: `src\ApiGateways\OcelotApiGw\ocelot.json`
- Event message: `src\BuildingBlocks\EventBus.Messages\Events\BasketCheckoutEvent.cs`
- gRPC proto: `src\Services\Basket.API\Protos\discount.proto`

Diagram

See `system-architecture.svg` for a simple visual diagram showing services, datastores and the event bus. If you cannot open the SVG, the ASCII diagram below gives a quick view:

Client -> API Gateway -> {Catalog, Basket, Discount, Ordering}
Datastores: Catalog->MongoDB, Basket->Redis, Discount->Postgres/SQL, Ordering->SQL Server
Event Bus (RabbitMQ) connects producers and consumers for async workflows

Scaling and deployment

- Services are independent and can be scaled by instance count; databases and RabbitMQ may require separate scaling considerations.
- Prefer stateless service instances; keep state in datastores.

