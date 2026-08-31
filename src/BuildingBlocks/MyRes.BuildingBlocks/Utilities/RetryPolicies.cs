using Polly;
using Polly.Retry;

namespace MyRes.BuildingBlocks.Utilities
{
    public static class RetryPolicies
    {
        public static AsyncRetryPolicy Fixed(int maxRetires, int delayMs, params Type[] exceptionTypes)
            => Policy
                .Handle<Exception>(ex => IsMatch(ex, exceptionTypes))
                .WaitAndRetryAsync(
                    maxRetires,
                    _ => TimeSpan.FromMilliseconds(delayMs),
                    onRetry: OnRetry
                );

        public static AsyncRetryPolicy Linear(int maxRetires, int delayMs, params Type[] exceptionTypes)
            => Policy
                .Handle<Exception>(ex => IsMatch(ex, exceptionTypes))
                .WaitAndRetryAsync(
                    maxRetires,
                    retryAttempt => TimeSpan.FromMilliseconds(delayMs * retryAttempt),
                    onRetry: OnRetry
                );

        public static AsyncRetryPolicy Exponential(int maxRetires, int delayMs, params Type[] exceptionTypes)
            => Policy
                .Handle<Exception>(ex => IsMatch(ex, exceptionTypes))
                .WaitAndRetryAsync(
                    maxRetires,
                    retryAttempt => TimeSpan.FromMilliseconds(delayMs * Math.Pow(2, retryAttempt - 1)),
                    onRetry: OnRetry
                );

        private static bool IsMatch(Exception ex, Type[] types)
        {
            return types.Any(t => t.IsAssignableFrom(ex.GetType()));
        }

        private static void OnRetry(Exception ex, TimeSpan ts, int retry, Context ctx)
        {
            Console.WriteLine($"Retry {retry} after {ts.TotalSeconds}s due to: {ex.Message}");
        }
    }
}
