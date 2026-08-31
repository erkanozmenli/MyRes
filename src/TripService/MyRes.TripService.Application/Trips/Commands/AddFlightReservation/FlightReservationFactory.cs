using MyRes.TripService.Application.Trips.Commands.AddFlightReservation.DTOs;
using MyRes.TripService.Domain.Entities;

namespace MyRes.TripService.Application.Trips.Commands.AddFlightReservation
{
    internal static class FlightReservationFactory
    {
        internal static FlightReservation Create(FlightReservationDto flightReservation)
        {
            var flights = flightReservation.Flights
                .Select(BuildFlight)
                .ToList();

            return FlightReservation.Create(flightReservation.TripType, flights);
        }

        private static Flight BuildFlight(FlightDto flight)
        {
            var segments = BuildSegments(flight.Segments);
            return Flight.Create(flight.Direction, segments);
        }

        private static List<FlightSegment> BuildSegments(IEnumerable<FlightSegmentDto> flightSegments)
        {
            var segments = new List<FlightSegment>();

            FlightSegment? previousSegment = null;

            foreach (var flightSegment in flightSegments)
            {
                var newSegment = FlightSegment.Create(
                    flightSegment.From,
                    flightSegment.To,
                    flightSegment.Departure,
                    flightSegment.Arrival,
                    previousSegment);

                segments.Add(newSegment);

                previousSegment = newSegment;
            }

            return segments;
        }
    }
}
