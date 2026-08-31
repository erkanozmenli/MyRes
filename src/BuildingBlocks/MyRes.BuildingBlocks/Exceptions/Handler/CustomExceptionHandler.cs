using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ValidationException = FluentValidation.ValidationException;

namespace MyRes.BuildingBlocks.Exceptions.Handler
{
    public sealed record ErrorInfo(int StatusCode, string Title, string Detail)
    {
        public string? Code { get; init; }
    }

    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            logger.LogError("Error Message: {Message}, Time: {Time}", exception.Message, DateTime.UtcNow);

            var error = Map(exception);

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = error.StatusCode;

            var problemDetails = new ProblemDetails
            {
                Title = error.Title,
                Detail = error.Detail,
                Status = error.StatusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["code"] = error.Code;
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions["validationErrors"] =
                    validationException.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(x => new
                            {
                                field = GetFieldName(x.PropertyName),
                                code = x.ErrorCode,
                                message = x.ErrorMessage
                            }).ToArray());
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static ErrorInfo Map(Exception exception)
        {
            return exception switch
            {
                InternalServerException =>
                    new ErrorInfo(
                        StatusCodes.Status500InternalServerError,
                        "Internal server error",
                        exception.Message)
                    {
                        Code = "request.internal_server_error"
                    },

                ValidationException =>
                    new ErrorInfo(
                        StatusCodes.Status422UnprocessableEntity,
                        "Validation failed",
                        "One or more validation errors occurred.")
                    {
                        Code = "request.validation_failed"
                    },

                BadHttpRequestException =>
                    new ErrorInfo(
                    StatusCodes.Status400BadRequest,
                        "Invalid request body",
                        exception.Message)
                    {
                        Code = "request.invalid_body"
                    },

                NotFoundException =>
                    new ErrorInfo(
                        StatusCodes.Status404NotFound,
                        "Resource not found",
                        exception.Message)
                    {
                        Code = "request.not_found"
                    },

                DomainException domainEx =>
                    new ErrorInfo(
                        StatusCodes.Status422UnprocessableEntity,
                        "Business rule violation",
                        domainEx.Message
                        )
                    {
                        Code = domainEx.Code
                    },

                _ =>
                    new ErrorInfo(
                        StatusCodes.Status500InternalServerError,
                        exception.GetType().Name,
                        exception.Message)
                    {
                        Code = "request.failed"
                    }
            };
        }

        private static string GetFieldName(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return string.Empty;

            if (!propertyName.Contains('.'))
                return propertyName;

            return propertyName
                .Split('.')
                .Last();
        }
    }
}
