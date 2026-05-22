# Docker and Compose notes

This file documents how Docker images and docker-compose are used for local development.

Build the entire stack

From the repository root:

Docker-compose up --build

Build a single service image

Docker build -f src/Services/Catalog.API/Dockerfile -t bookstore/catalog.api:local .

Compose overrides

- You can create a `docker-compose.override.yml` in the repo root to customize services for development (volumes, build context overrides, etc.).

Environment variables

- `docker-compose.yml` supplies environment variables to each container. When running services locally outside Docker, set corresponding env vars through your IDE.

Publishing images

1. Tag the image: `docker tag bookstore/catalog.api:local myregistry/bookstore/catalog.api:v1.0.0`
2. Push: `docker push myregistry/bookstore/catalog.api:v1.0.0`

Troubleshooting

- If a container fails to start because a dependency isn't ready (e.g., DB), inspect logs and consider adding a simple wait-for mechanism in entrypoint scripts.

