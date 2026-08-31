namespace MyRes.AppHost
{
    public static class ResourceBuilderExtensions
    {
        public static IResourceBuilder<ProjectResource> WithOpenTelemetry(
            this IResourceBuilder<ProjectResource> project,
            IDistributedApplicationBuilder builder,
            OpenTelemetryOptions options)
        {
            if (options is null)
                throw new InvalidOperationException("OpenTelemetry configuration not found.");

            if (!options.UseAspire)
            {
                project = project.WithEnvironment("OTEL_EXPORTER_OTLP_ENDPOINT", options.CollectorEndpoint)
                                 .WithEnvironment("OTEL_EXPORTER_OTLP_PROTOCOL", "grpc");
            }

            return project;
        }
    }
}
