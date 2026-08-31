using Carter;
using MyRes.BuildingBlocks.Api.Exceptions;
using MyRes.BuildingBlocks.Authentication;
using MyRes.BuildingBlocks.Exceptions.Handler;
using MyRes.BuildingBlocks.Infrastructure.Extensions;
using MyRes.NotificationService.Infrastructure.Notifications.WebSocket;
using Scalar.AspNetCore;

namespace MyRes.NotificationService.Api
{
    public static class ApiExtensions
    {
        public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddProblemDetails();

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddOpenApi();

            builder.Services.AddOpenApi(ApiDocConstants.Version, options =>
            {
                options.AddDocumentTransformer((doc, ctx, ct) =>
                {
                    doc.Info.Title = ApiDocConstants.Title;
                    doc.Info.Version = ApiDocConstants.Version;
                    doc.Info.Description = ApiDocConstants.Description;

                    foreach (var server in doc.Servers ?? [])
                        server.Url = server.Url?.TrimEnd('/');

                    return Task.CompletedTask;
                });
            });

            builder.Services.AddCarter();

            builder.Services.AddGatewayIdentity();

            return builder;
        }

        public static WebApplication UseApi(this WebApplication app)
        {
            app.UseExceptionHandler();

            app.UseApiRequestLogging();

            app.UseCustomStatusCodePages();

            app.MapCarter();

            app.UseGatewayIdentity();

            //app.Use(async (context, next) =>
            //{
            //    var pod = Environment.GetEnvironmentVariable("HOSTNAME");

            //    var connectionId = context.Request.Query["id"].ToString();

            //    app.Logger.LogInformation(
            //        "Trace={Trace} {Method} {Path} id={Id} Pod={Pod}",
            //        context.TraceIdentifier,
            //        context.Request.Method,
            //        context.Request.Path,
            //        connectionId,
            //        pod);

            //    await next();
            //});

            app.MapSystemEndpoints(app.Configuration);

            app.MapOpenApi();

            app.MapScalarApiReference(options =>
            {
                options.AddDocument(
                    ApiDocConstants.Versions.V1.Name,
                    ApiDocConstants.Versions.V1.DisplayName,
                    ApiDocConstants.Versions.V1.JsonPath,
                    isDefault: true);
            });

            app.Logger.LogInformation("Mapping NotificationHub");

            app.MapHub<NotificationHub>("/v1/notifications/hub")
                 .WithDisplayName("NotificationHub");

            var dataSource = app.Services.GetRequiredService<EndpointDataSource>();
            foreach (var endpoint in dataSource.Endpoints)
            {
                app.Logger.LogInformation("{Endpoint}", endpoint.DisplayName);
            }

            return app;
        }

        private static IEndpointRouteBuilder MapSystemEndpoints(this IEndpointRouteBuilder endpoints, IConfiguration configuration)
        {
            var serviceName = configuration["Service:Name"];

            endpoints.MapGet("/", () => Results.Redirect("/scalar"))
                .ExcludeFromDescription();


            endpoints.MapGet("health/instance", () =>
            {
                return Results.Ok(Environment.MachineName);
            });


            endpoints.MapGet("/health/ready", () =>
            {
                return Results.Ok($"{serviceName} readiness check OK");
            })
            .WithTags("Health")
            .ExcludeFromDescription();

            return endpoints;
        }
    }
}
