using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MyRes.BuildingBlocks.Api.Exceptions
{
    public class StatusCodePagesMiddleware
    {
        private readonly RequestDelegate _next;

        public StatusCodePagesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.HasStarted)
                return;

            var statusCode = context.Response.StatusCode;

            if (statusCode < 400)
                return;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(statusCode),
                Instance = context.Request.Path
            };

            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["code"] = GetCode(statusCode);

            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problemDetails));
        }

        private static string GetTitle(int statusCode) =>
        statusCode switch
        {
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status403Forbidden => "Forbidden",
            StatusCodes.Status404NotFound => "Resource not found",
            StatusCodes.Status405MethodNotAllowed => "Method not allowed",
            StatusCodes.Status415UnsupportedMediaType => "Unsupported media type",
            _ => "Unknown Error"
        };

        private static string GetCode(int statusCode) =>
    statusCode switch
    {
        StatusCodes.Status401Unauthorized => "request.unauthorized",
        StatusCodes.Status403Forbidden => "request.forbidden",
        StatusCodes.Status404NotFound => "request.route_not_found",
        StatusCodes.Status405MethodNotAllowed => "request.method_not_allowed",
        StatusCodes.Status415UnsupportedMediaType => "request.unsupported_media_type",

        StatusCodes.Status500InternalServerError => "request.internal_server_error",
        StatusCodes.Status502BadGateway => "request.bad_gateway",
        StatusCodes.Status503ServiceUnavailable => "request.service_unavailable",
        StatusCodes.Status504GatewayTimeout => "request.gateway_timeout",
        _ => "request.unknown_error"
    };
    }
}
