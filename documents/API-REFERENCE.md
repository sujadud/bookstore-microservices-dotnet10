# API Reference (compact)

This file gives a short, discoverable list of the important endpoints and where to find request/response models.

Where to find live docs

- Start the stack and open the API Gateway Swagger UI (gateway port usually in `src\ApiGateways\OcelotApiGw`).

Services and key endpoints

- Catalog Service (`src\Services\Catalog.API`)
  - GET /api/catalog — list books
  - GET /api/catalog/{id} — get book details
  - POST /api/catalog — create book
  - Models: `src\Services\Catalog.API\Models\Book.cs`

- Basket Service (`src\Services\Basket.API`)
  - GET /api/basket/{userId}
  - POST /api/basket — add / update basket
  - POST /api/basket/checkout — checkout and publish `BasketCheckoutEvent`

- Discount Service (`src\Services\Discount.API` and `src\Services\Discount.Grpc`)
  - HTTP: GET/POST /api/discount
  - gRPC implementation: see `src\Services\Discount.Grpc` and `src\Services\Basket.API\Protos\discount.proto`

- Ordering Service (`src\Services\Ordering\Ordering.API`)
  - POST /api/order — create order (consumes BasketCheckoutEvent in the event-driven flow)

Example files

- See `.http` example files under `src/*/*.http` for ready-to-run HTTP examples.

