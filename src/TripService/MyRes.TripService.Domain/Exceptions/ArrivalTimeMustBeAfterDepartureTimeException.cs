using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class ArrivalTimeMustBeAfterDepartureTimeException : DomainException
    {
        public ArrivalTimeMustBeAfterDepartureTimeException() :
            base(
                    "trip.flightreservation.flight.flightsegment.arrival_time_must_be_after_departure_time",
                    "Arrival time must be after departure time."
                )
        {

        }
    }
}
