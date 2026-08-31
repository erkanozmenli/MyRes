using MassTransit.Logging;
using MassTransit.Monitoring;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Diagnostics.Metrics;


namespace MyRes.BuildingBlocks.Infrastructure.Extensions
{
    public static class TelemetryExtensions
    {
        public static IServiceCollection AddTelemetry(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var serviceName = configuration["Service:Name"];
            var serviceVersion = configuration["Service:Version"];

            configuration["OTEL_SERVICE_NAME"] = serviceName;
            configuration["OTEL_RESOURCE_ATTRIBUTES"] = $"service.version={serviceVersion}, deployment.environment.name={environment.EnvironmentName}";

            var activitySource = new ActivitySource($"{serviceName}");
            var meter = new Meter($"{serviceName}");

            services.AddSingleton(activitySource);
            services.AddSingleton(meter);

            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder
                        .AddSource(activitySource.Name)
                        .AddAspNetCoreInstrumentation(options =>
                        {
                            options.Filter = context =>
                            {
                                var path = context.Request.Path;
                                return !(path.StartsWithSegments("/health"));
                            };
                        })
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation(options =>
                        {
                            options.Filter = command =>
                            {
                                if (command is not Microsoft.Data.SqlClient.SqlCommand sqlCommand)
                                    return true;

                                var sql = sqlCommand.CommandText;

                                return !sql.Contains("OutboxState", StringComparison.OrdinalIgnoreCase)
                                    && !sql.Contains("InboxState", StringComparison.OrdinalIgnoreCase)
                                    && !sql.Contains("OutboxMessage", StringComparison.OrdinalIgnoreCase);
                            };
                        })
                        .AddSource(DiagnosticHeaders.DefaultListenerName)
                        .AddOtlpExporter();
                })
                .WithMetrics(builder =>
                {
                    builder
                        .AddMeter(meter.Name)
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation()
                        .AddMeter(InstrumentationOptions.MeterName)
                        .AddOtlpExporter();
                });

            return services;
        }
    }
}
