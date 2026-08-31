using MyRes.BuildingBlocks.Exceptions;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Domain.Exceptions
{
    public class FlightSegmentDatesOverlapException : DomainException
    {
        public FlightSegmentDatesOverlapException(FlightSegment previousSegment, FlightSegment currentSegment)
            : base(
                "trip.flightreservation.flight.flightsegment.overlap",
                $"Flight segment dates overlap with an existing segment." +
                $"First Leg: {previousSegment.FromAirport}-{previousSegment.ToAirport} {previousSegment.ArrivalTime:g} " +
                $"Second Leg: {currentSegment.FromAirport}-{currentSegment.ToAirport} {currentSegment.DepartureTime:g} ")
        {

        }
    }
}
