using MyRes.TripService.Domain.Entities;
using MyRes.TripService.Domain.Entities.AggregateRoots;
using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Infrastructure.ContractTests.ContractTests.Data
{
    public static class TripData
    {
        public static IEnumerable<FlightReservation> FlightReservations =>
            [
                CreateRoundTripWithConnecting(),
                CreateOneWayWithConnecting(),
                CreateRoundTripTransatlantic()
            ];

        public static IEnumerable<Trip> Trips => CreateTripsWithFlightReservations();

        private static FlightReservation CreateRoundTripWithConnecting()
        {
            var ordToJfk = FlightSegment.Create("ORD", "JFK",
                new DateTime(2024, 06, 15, 05, 00, 0),
                new DateTime(2024, 06, 15, 08, 00, 0));
            var jfkToLax = FlightSegment.Create("JFK", "LAX",
                new DateTime(2024, 06, 15, 08, 00, 0),
                new DateTime(2024, 06, 15, 11, 30, 0),
                ordToJfk);

            var laxToJfk = FlightSegment.Create("LAX", "JFK",
                new DateTime(2024, 06, 22, 14, 00, 0),
                new DateTime(2024, 06, 22, 23, 00, 0));

            return FlightReservation.Create(TripType.RoundTrip,
            [
                Flight.Create(FlightDirection.Outbound, [ordToJfk, jfkToLax]),
                Flight.Create(FlightDirection.Inbound, [laxToJfk])
            ]);
        }

        private static FlightReservation CreateOneWayWithConnecting()
        {
            var dfwToJfk = FlightSegment.Create("DFW", "JFK",
                new DateTime(2024, 07, 10, 08, 00, 0),
                new DateTime(2024, 07, 10, 10, 00, 0));
            var jfkToMia = FlightSegment.Create("JFK", "MIA",
                new DateTime(2024, 07, 10, 10, 00, 0),
                new DateTime(2024, 07, 10, 12, 30, 0),
                dfwToJfk);

            return FlightReservation.Create(TripType.OneWay,
            [
                Flight.Create(FlightDirection.Outbound, [dfwToJfk, jfkToMia])
            ]);
        }

        private static FlightReservation CreateOneWayWithConnecting2()
        {
            var dfwToJfk = FlightSegment.Create("LHR", "BOS",
                new DateTime(2024, 07, 10, 12, 30, 0),
                new DateTime(2024, 07, 10, 14, 00, 0));
            var jfkToMia = FlightSegment.Create("BOS", "JFK",
                new DateTime(2024, 07, 10, 14, 00, 0),
                new DateTime(2024, 07, 10, 16, 30, 0),
                dfwToJfk);

            return FlightReservation.Create(TripType.OneWay,
            [
                Flight.Create(FlightDirection.Outbound, [dfwToJfk, jfkToMia])
            ]);
        }

        private static FlightReservation CreateRoundTripTransatlantic()
        {
            var jfkToBos = FlightSegment.Create("JFK", "BOS",
                new DateTime(2024, 08, 01, 06, 00, 0),
                new DateTime(2024, 08, 01, 07, 30, 0));
            var bosToLhr = FlightSegment.Create("BOS", "LHR",
                new DateTime(2024, 08, 01, 09, 00, 0),
                new DateTime(2024, 08, 01, 20, 00, 0),
                jfkToBos);

            var lhrToBos = FlightSegment.Create("LHR", "BOS",
                new DateTime(2024, 08, 10, 08, 00, 0),
                new DateTime(2024, 08, 10, 11, 30, 0));
            var bosToJfk = FlightSegment.Create("BOS", "JFK",
                new DateTime(2024, 08, 10, 13, 00, 0),
                new DateTime(2024, 08, 10, 14, 30, 0),
                lhrToBos);

            return FlightReservation.Create(TripType.RoundTrip,
            [
                Flight.Create(FlightDirection.Outbound, [jfkToBos, bosToLhr]),
                Flight.Create(FlightDirection.Inbound, [lhrToBos, bosToJfk])
            ]);
        }

        private static IEnumerable<Trip> CreateTripsWithFlightReservations()
        {
            var trips = new List<Trip>();

            var note = "contract test";
            var userId = Guid.NewGuid();

            foreach (var fr in FlightReservations)
            {
                var trip = Trip.Create(note, userId);
                trip.AddFlightReservation(fr);
                trips.Add(trip);
            }

            var trip2 = Trip.Create(note, userId);
            trip2.AddFlightReservation(CreateOneWayWithConnecting());
            trip2.AddFlightReservation(CreateOneWayWithConnecting2());
            trips.Add(trip2);

            return trips.AsEnumerable();
        }
    }
}
