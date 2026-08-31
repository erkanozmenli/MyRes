using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace MyRes.BuildingBlocks.Infrastructure.Extensions
{
    public static class SerilogExtensions
    {
        public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();

            var serviceName = builder.Configuration["Service:Name"];
            var serviceVersion = builder.Configuration["Service:Version"];
            var environment = builder.Environment.EnvironmentName;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .MinimumLevel.Override("MassTransit", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.OpenTelemetry(options =>
                {
                    options.ResourceAttributes = new Dictionary<string, object>
                    {
                        ["service.name"] = serviceName!,
                        ["service.version"] = serviceVersion!,
                        ["deployment.environment.name"] = environment
                    };
                })
                .CreateLogger();

            builder.Host.UseSerilog();

            return builder;
        }

        public static WebApplication UseApiRequestLogging(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (ctx, elapsed, ex) =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/health"))
                        return LogEventLevel.Verbose;

                    return ex != null ? LogEventLevel.Error : LogEventLevel.Information;
                };
            });

            return app;
        }
    }
}
