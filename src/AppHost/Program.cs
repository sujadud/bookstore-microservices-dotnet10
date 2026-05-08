using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add shared resources
var mongodb = builder.AddMongoDB("mongodb")
    .WithDataVolume();

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

var redis = builder.AddRedis("redis");

var rabbitmq = builder.AddRabbitMQ("rabbitmq");

// Add Catalog.API (MongoDB)
var catalogApi = builder.AddProject("catalog-api", "..\\Services\\Catalog.API\\Catalog.API.csproj")
    .WithReference(mongodb)
    .WithHttpEndpoint(targetPort: 8080, name: "catalog-http")
    .WithEnvironment("ASPNETCORE_URLS", "http://+:8080");

// Add Discount.API (PostgreSQL)
var discountApi = builder.AddProject("discount-api", "..\\Services\\Discount.API\\Discount.API.csproj")
    .WithReference(postgres)
    .WithHttpEndpoint(targetPort: 8081, name: "discount-http")
    .WithEnvironment("ASPNETCORE_URLS", "http://+:8081");

// Add Discount.Grpc (PostgreSQL) - gRPC service
var discountGrpc = builder.AddProject("discount-grpc", "..\\Services\\Discount.Grpc\\Discount.Grpc.csproj")
    .WithReference(postgres)
    .WithHttpEndpoint(targetPort: 8090, name: "discount-grpc-http")
    .WithEnvironment("ASPNETCORE_URLS", "http://+:8090");

// Add Ordering.API (PostgreSQL + RabbitMQ)
var orderingApi = builder.AddProject("ordering-api", "..\\Services\\Ordering\\Ordering.API\\Ordering.API.csproj")
    .WithReference(postgres)
    .WithReference(rabbitmq)
    .WithHttpEndpoint(targetPort: 8082, name: "ordering-http")
    .WithEnvironment("ASPNETCORE_URLS", "http://+:8082");

// Add Basket.API (Redis + RabbitMQ + gRPC reference to Discount.Grpc)
var basketApi = builder.AddProject("basket-api", "..\\Services\\Basket.API\\Basket.API.csproj")
    .WithReference(redis)
    .WithReference(discountGrpc)
    .WithReference(rabbitmq)
    .WithHttpEndpoint(targetPort: 8083, name: "basket-http")
    .WithEnvironment("ASPNETCORE_URLS", "http://+:8083");

// Add OcelotApiGw as entry point - references all services for routing
var apiGateway = builder.AddProject("api-gateway", "..\\ApiGateways\\OcelotApiGw\\OcelotApiGw.csproj")
    .WithReference(catalogApi)
    .WithReference(basketApi)
    .WithReference(discountApi)
    .WithReference(orderingApi)
    .WithHttpEndpoint(targetPort: 5000, name: "gateway-http", port: 5000)
    .WithEnvironment("ASPNETCORE_URLS", "http://+:5000");

builder.Build().Run();

