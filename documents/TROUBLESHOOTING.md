# Troubleshooting

Common problems and remediation steps.

1. Ports in use

- Symptom: docker-compose or a service fails because a port is already bound.
- Fix: identify the process and stop it (Windows PowerShell):

  Get-Process -Id (Get-NetTCPConnection -LocalPort 5001).OwningProcess

  Or change the port mapping in `docker-compose.yml` or the service's `appsettings.json`/launch profile.

2. Database not ready / connection refused

- Symptom: Service cannot connect to Mongo/Postgres/Redis on startup.
- Fix: wait for the DB container to become healthy, inspect logs: `docker-compose logs -f <service>`; consider retry/backoff logic in the consumer service.

3. RabbitMQ connectivity

- Symptom: Events are not being published or consumed.
- Fix: confirm RabbitMQ is up, check credentials in `docker-compose.yml`, and inspect consumer logs for handshake errors.

4. gRPC issues

- Symptom: gRPC client fails to call service.
- Fix: confirm the gRPC port and that TLS/HTTP/2 settings match client expectations. Use `grpcurl -plaintext` for testing if TLS is not used.

5. Container image build failures

- Symptom: `docker build` fails with SDK or dependency errors.
- Fix: ensure the .NET SDK version in the Dockerfile matches your local/environment expectations; clear Docker build cache and retry: `docker builder prune`.

Collecting logs

- docker-compose logs -f <service>
- For local runs: inspect console output from `dotnet run` or IDE run window.

