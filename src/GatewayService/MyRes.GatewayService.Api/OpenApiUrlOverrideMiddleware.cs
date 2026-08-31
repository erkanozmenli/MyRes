using MyRes.GatewayService.Api.Configuration;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MyRes.GatewayService.Api
{
    public sealed class OpenApiUrlOverrideMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IReadOnlySet<PathString> _overrideRoutes;
        private readonly ILogger<OpenApiUrlOverrideMiddleware> _logger;

        public OpenApiUrlOverrideMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<OpenApiUrlOverrideMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            _overrideRoutes = configuration
                                .GetSection("OpenApiDocuments")
                                .Get<List<OpenApiDocumentOptions>>()?
                                .Where(x => x.UrlOverride)
                                .Select(x => new PathString(x.Route))
                                .ToHashSet() ?? [];
        }

        public async Task Invoke(HttpContext context)
        {
            if (!_overrideRoutes.Contains(context.Request.Path))
            {
                await _next(context);
                return;
            }

            var originalBody = context.Response.Body;

            await using var buffer = new MemoryStream();
            context.Response.Body = buffer;

            await _next(context);

            buffer.Position = 0;

            var json = await JsonNode.ParseAsync(buffer);

            if (json is JsonObject obj)
            {
                var gatewayUrl = $"{context.Request.Scheme}://{context.Request.Host}";

                obj["servers"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["url"] = gatewayUrl
                    }
                };
            }

            context.Response.Body = originalBody;
            context.Response.ContentLength = null;

            await JsonSerializer.SerializeAsync(context.Response.Body, json);
        }
    }
}
