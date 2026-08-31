using MyRes.TripService.Application.Queries.Shared.Models;
using MyRes.TripService.Application.Trips.Queries.Shared.DTOs;
using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Application.Queries.Shared.Projections
{
    public static class FlightReservationProjection
    {
        public static FlightReservationDto ToDto(this IEnumerable<FlightReservationFlatRow> flatRows)
        {
            var rows = flatRows.ToList();

            if (rows.Count == 0)
                throw new ArgumentException("Flat rows cannot be empty.", nameof(flatRows));

            var tripItemId = rows[0].TripItemId;
            var tripType = rows[0].TripType;

            var flights = BuildFlights(rows);

            return new FlightReservationDto
            {
                Id = tripItemId,
                TripType = tripType,
                Flights = flights
            };
        }

        private static List<FlightDto> BuildFlights(List<FlightReservationFlatRow> rows)
        {
            var flightGroups = new Dictionary<int, List<FlightReservationFlatRow>>();

            // 1. Grouping (foreach)
            foreach (var row in rows)
            {
                if (!flightGroups.ContainsKey(row.FlightId))
                {
                    flightGroups[row.FlightId] = new List<FlightReservationFlatRow>();
                }

                flightGroups[row.FlightId].Add(row);
            }

            // 2. Map flights
            var flights = new List<FlightDto>();

            foreach (var group in flightGroups)
            {
                var flightRows = group.Value;

                var flight = new FlightDto
                {
                    Direction = (FlightDirection)flightRows[0].Direction,
                    Segments = BuildSegments(flightRows)
                };

                flights.Add(flight);
            }

            // 3. Order
            flights.Sort((a, b) => a.Direction.CompareTo(b.Direction));

            return flights;
        }

        private static List<FlightSegmentDto> BuildSegments(List<FlightReservationFlatRow> rows)
        {
            var result = new List<FlightSegmentDto>();

            FlightReservationFlatRow? current = null;

            // find root
            foreach (var row in rows)
            {
                if (row.PreviousSegmentId == null)
                {
                    current = row;
                    break;
                }
            }

            if (current == null)
                return result;

            result.Add(Map(current));

            while (true)
            {
                FlightReservationFlatRow? next = null;

                foreach (var row in rows)
                {
                    if (row.PreviousSegmentId == current!.FlightSegmentId)
                    {
                        next = row;
                        break;
                    }
                }

                if (next == null)
                    break;

                result.Add(Map(next));
                current = next;
            }

            return result;
        }

        private static FlightSegmentDto Map(FlightReservationFlatRow row)
        {
            return new FlightSegmentDto
            {
                Id = row.FlightSegmentId,
                PreviousSegmentId = row.PreviousSegmentId,
                From = row.FromAirport,
                To = row.ToAirport,
                Departure = row.DepartureTime,
                Arrival = row.ArrivalTime
            };
        }
    }
}
