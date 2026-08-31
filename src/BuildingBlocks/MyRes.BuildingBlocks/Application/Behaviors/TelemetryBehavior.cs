using MediatR;
using System.Diagnostics;

namespace MyRes.BuildingBlocks.Application.Behaviors
{
    public sealed class TelemetryBehavior<TRequest, TResponse>(ActivitySource activitySource)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestType = typeof(TRequest);
            using var activity = activitySource.StartActivity(requestType.Name, ActivityKind.Internal);

            activity?.SetTag("myres.cqrs.request.name", requestType.Name);
            activity?.SetTag("myres.cqrs.request.type", requestType.FullName);

            try
            {
                var response = await next();
                activity?.SetStatus(ActivityStatusCode.Ok);
                return response;
            }
            catch (Exception ex)
            {
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                throw;
            }
        }
    }
}
