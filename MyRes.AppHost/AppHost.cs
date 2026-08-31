using Microsoft.Extensions.Configuration;
using MyRes.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
var options = new AppHostOptions();
builder.Configuration.Bind(options);

// Open Telemetry Collector
var collector = builder.AddContainer("otel-collector", "otel/opentelemetry-collector-contrib", "0.156.0")
    .WithBindMount("./datamount/otel", "/etc/otel", isReadOnly: true)
    .WithArgs("--config=/etc/otel/config.yaml")
    .WithEndpoint(name: "otlp-grpc", port: 4317, targetPort: 4317, scheme: "http");

// RabbitMQ
var rabbitMq = builder.AddRabbitMQ(name: "rabbitmq", port: 5672).WithImageTag("4.2-management").WithManagementPlugin();

// Redis
var redis = builder.AddRedis("redis").WithImage("redis").WithImageTag("7.4.7");

// Redis Insight
builder.AddContainer("redis-insight", "redis/redisinsight")
    .WithReference(redis)
    .WithHttpEndpoint(targetPort: 5540)
    .WithImageTag("3.8.0")
    .WithVolume("redisinsight-data", "/data");

// Keycloak
var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak", "26.6.1")
    .WithBindMount("./datamount/keycloak", "/opt/keycloak/data/import")
    .WithEndpoint(name: "identity-server", port: 8080, targetPort: 8080, scheme: "http")
    .WithArgs("start-dev", "--import-realm")
    .WithEnvironment("KC_BOOTSTRAP_ADMIN_USERNAME", "admin")
    .WithEnvironment("KC_BOOTSTRAP_ADMIN_PASSWORD", "admin")
    .WithVolume("keycloak-data", "/opt/keycloak/data");

// Services
var tripService = builder.AddProject<Projects.MyRes_TripService_Api>(Constants.TripService)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithOpenTelemetry(builder, options.OpenTelemetry);

var paymentService = builder.AddProject<Projects.MyRes_PaymentService_Api>(Constants.PaymentService)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithOpenTelemetry(builder, options.OpenTelemetry);

var providerService = builder.AddProject<Projects.MyRes_ProviderService_Api>(Constants.ProviderService)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithOpenTelemetry(builder, options.OpenTelemetry);

var notificationService = builder.AddProject<Projects.MyRes_NotificationService_Api>(Constants.NotificationService)
    .WithReference(rabbitMq)
    .WithReference(redis)
    .WaitFor(rabbitMq)
    .WaitFor(redis)
    .WithOpenTelemetry(builder, options.OpenTelemetry);


if (!options.UseLocalDb)
{
    // Aspire MS Sql Server
    var sqlPassword = builder.AddParameter("pw", options.Parameters.SqlPassword);
    var sqlServer = builder.AddSqlServer(name: Constants.MyResAspireDBServer, password: sqlPassword, port: 20001);

    // Aspire Databases
    var tripServiceDb = sqlServer.AddDatabase(Constants.TripServiceDb);
    var paymentServiceDb = sqlServer.AddDatabase(Constants.PaymentServiceDb);
    var providerServiceDb = sqlServer.AddDatabase(Constants.ProviderServiceDb);
    var notificationServiceDb = sqlServer.AddDatabase(Constants.NotificationServiceDb);

    // Aspire TripService Project
    tripService
        .WithReference(tripServiceDb, connectionName: Constants.TripServiceConnection)
        .WaitFor(tripServiceDb);

    // Aspire PaymentService Project
    paymentService
        .WithReference(paymentServiceDb, connectionName: Constants.PaymentServiceConnection)
        .WaitFor(paymentServiceDb);

    // Aspire ProviderService Project
    providerService
        .WithReference(providerServiceDb, connectionName: Constants.ProviderServiceConnection)
        .WaitFor(providerServiceDb);

    // Aspire NotificationService Project
    notificationService
        .WithReference(notificationServiceDb, connectionName: Constants.NotificationServiceConnection)
        .WaitFor(notificationServiceDb);
}


// Gateway (YARP) / BFF
var gateway = builder.AddProject<Projects.MyRes_GatewayService_Api>(Constants.GatewayService)
        .WithReference(redis)
        .WaitFor(redis)
        .WithOpenTelemetry(builder, options.OpenTelemetry);

// React Frontend Application
if (!options.UseExternalFrontend)
{
    var frontend = builder.AddViteApp("frontend", "../frontend/web-app")
            .WithEnvironment("GATEWAY_URL", gateway.GetEndpoint("https"))
            .WithEndpoint(
                "http",
                endpoint =>
                {
                    endpoint.Port = 7000;
                }
            );
}

builder.Build().Run();
