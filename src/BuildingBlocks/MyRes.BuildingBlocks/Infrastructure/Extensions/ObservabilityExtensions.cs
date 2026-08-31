using Microsoft.AspNetCore.Builder;

namespace MyRes.BuildingBlocks.Infrastructure.Extensions
{
    public static class ObservabilityExtensions
    {
        public static WebApplicationBuilder AddObservability(this WebApplicationBuilder builder)
        {
            builder.AddSerilogLogging();
            builder.Services.AddTelemetry(builder.Configuration, builder.Environment);
            return builder;
        }
    }
}
