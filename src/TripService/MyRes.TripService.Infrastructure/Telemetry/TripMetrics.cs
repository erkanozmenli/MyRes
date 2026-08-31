using MyRes.TripService.Application.Abstractions.Metrics;
using System.Diagnostics.Metrics;

namespace MyRes.TripService.Infrastructure.Telemetry
{
    public class TripMetrics : ITripMetrics
    {
        private readonly Counter<long> _tripRetrievedCounter;
        private readonly Histogram<int> _tripItemsHistogram;

        public TripMetrics(Meter meter)
        {
            _tripRetrievedCounter =
            meter.CreateCounter<long>(
                "trip.retrieved.count",
                unit: "trip",
                description: "Number of successfully retrieved trips");


            _tripItemsHistogram =
                meter.CreateHistogram<int>(
                    "trip.items.count",
                    unit: "items",
                    description: "Number of items in a retrieved trip");
        }

        public void TripRetrieved(Guid tripId, int itemCount)
        {
            var tags = new[]
            {
                new KeyValuePair<string, object?>("trip.id", tripId)
            };

            _tripRetrievedCounter.Add(1, tags);
            _tripItemsHistogram.Record(itemCount, tags);
        }
    }
}
