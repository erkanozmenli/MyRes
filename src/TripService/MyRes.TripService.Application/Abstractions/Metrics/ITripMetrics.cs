namespace MyRes.TripService.Application.Abstractions.Metrics
{
    public interface ITripMetrics
    {
        void TripRetrieved(Guid tripId, int itemCount);
    }
}
