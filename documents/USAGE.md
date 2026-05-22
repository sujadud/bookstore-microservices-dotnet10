# Usage and Examples

This file contains quick usage examples for exploring the running services.

Swagger and API Gateway

- After starting the stack, open the API Gateway Swagger UI (if enabled) at the gateway URL (e.g., http://localhost:5010/swagger). The exact port is configured in `src\ApiGateways\OcelotApiGw`.

HTTP examples (curl)

1. List catalog books (example):

   curl http://localhost:5001/api/catalog

2. Add item to basket (example):

   curl -X POST http://localhost:5002/api/basket -H "Content-Type: application/json" -d '{"userId":"test","items":[{"bookId":"1","quantity":1}]}'

3. Checkout (publishes event to RabbitMQ):

   curl -X POST http://localhost:5002/api/basket/checkout -H "Content-Type: application/json" -d '{"userId":"test","address":"..."}'

HTTP example files

There are .http example files included for convenience under `src/*/*.http` such as:

- `src\Services\Catalog.API\Catalog.API.http`
- `src\Services\Basket.API\Basket.API.http`

gRPC

- The Discount gRPC service exposes a proto at `src\Services\Basket.API\Protos\discount.proto` and in `src\Services\Discount.Grpc`.
- Use `grpcurl` or a gRPC client to call the service; when running in Docker, map the port defined in the service's `appsettings.json`.

Health checks

- If services expose health endpoints, use the gateway or direct service URL to check readiness and liveness.

Next steps

- See `API-REFERENCE.md` for a compact list of endpoints and models.

