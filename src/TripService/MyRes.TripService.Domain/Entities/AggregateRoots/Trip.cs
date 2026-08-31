using MyRes.Shared.Domain;
using System.Globalization;
using MyRes.TripService.Domain.Exceptions;
using MyRes.TripService.Domain.Enums;

namespace MyRes.TripService.Domain.Entities.AggregateRoots
{
    public class Trip : Aggregate<Guid>
    {
        public int TripNo { get; private set; }
        public string Note { get; private set; } = null!;
        public TripStatus Status { get; private set; }
        public bool IsWorkflowCompleted { get; private set; }
        public DateTime? WorkflowCompletedAt { get; private set; }
        public Guid UserId { get; private set; }


        private readonly List<TripItem> _lines = new();
        public IReadOnlyCollection<TripItem> Lines => _lines.AsReadOnly();

        private Trip() { }

        public static Trip Create(string note, Guid UserId)
        {
            var trip = new Trip();
            trip.UserId = UserId;
            trip.Note = note;
            trip.Status = TripStatus.Draft;

            return trip;
        }

        public void StartCheckout()
        {
            EnsureStatus(TripStatus.Draft);

            Status = TripStatus.CheckoutPending;
        }

        public void CompletePayment()
        {
            EnsureStatus(TripStatus.CheckoutPending);

            Status = TripStatus.PaymentCompleted;
        }

        public void CompleteBooking()
        {
            EnsureStatus(TripStatus.PaymentCompleted);

            Status = TripStatus.BookingCompleted;
            CompleteWorkflow();
        }

        public void FailPayment()
        {
            EnsureStatus(TripStatus.CheckoutPending);

            Status = TripStatus.PaymentFailed;
            CompleteWorkflow();
        }

        public void FailBooking()
        {
            EnsureStatus(TripStatus.PaymentCompleted);

            Status = TripStatus.BookingFailed;
        }

        //public void StartRefund()
        //{
        //    EnsureStatus(TripStatus.BookingFailed);

        //    Status = TripStatus.RefundPending;
        //}

        public void CompleteRefund()
        {
            EnsureStatus(TripStatus.BookingFailed);

            Status = TripStatus.Refunded;
            CompleteWorkflow();
        }

        private void CompleteWorkflow()
        {
            IsWorkflowCompleted = true;
            WorkflowCompletedAt = DateTime.UtcNow;
        }

        public void AddCarReservation(CarReservation car)
        {
            _lines.Add(car);
        }

        public void AddHotelReservation(HotelReservation hotel)
        {
            _lines.Add(hotel);
            EnsureNoHotelReservationOverlaps();
        }

        public void AddFlightReservation(FlightReservation flight)
        {
            _lines.Add(flight);
            EnsureNoFlightReservationOverlaps();
        }

        public void ChangeHotelDates(int reservationLineId, DateTimeOffset checkIn, DateTimeOffset checkOut)
        {
            var hotelLine = _lines.OfType<HotelReservation>().Single(x => x.Id == reservationLineId);
            hotelLine.ChangeDates(checkIn, checkOut);
        }

        private void EnsureNoHotelReservationOverlaps()
        {
            var hotels = _lines.OfType<HotelReservation>().OrderBy(h => h.CheckIn).ToList();

            for (int i = 1; i < hotels.Count; i++)
            {
                var current = hotels[i];
                var previous = hotels[i - 1];

                if (current.CheckIn < previous.CheckOut)
                {
                    throw new HotelReservationDatesOverlapException(
                        previous.CheckIn,
                        previous.CheckOut,
                        current.CheckIn,
                        current.CheckOut
                    );
                }
            }
        }

        public void ChangeFlightSegmentDates(int flightReservationId, int flightId, int flightSegmentId, DateTimeOffset departureTime, DateTimeOffset arrivalTime)
        {
            var flightReservation = _lines
                .OfType<FlightReservation>()
                .SingleOrDefault(fr => fr.Id == flightReservationId)
                ?? throw new FlightReservationDoesNotExistException(flightReservationId);

            var flight = _lines
                .OfType<FlightReservation>()
                .SelectMany(fr => fr.Flights)
                .SingleOrDefault(f => f.Id == flightId)
                ?? throw new FlightDoesNotExistException(flightId);

            var segment = flight.Segments
                .SingleOrDefault(x => x.Id == flightSegmentId)
                ?? throw new FlightSegmentDoesNotExistException(flightSegmentId);

            segment.ChangeDates(departureTime, arrivalTime);

            EnsureNoFlightReservationOverlaps();
        }

        private void EnsureNoFlightReservationOverlaps()
        {
            var culture = CultureInfo.CurrentCulture;

            var flightSegments = _lines.OfType<FlightReservation>()
                .SelectMany(fr => fr.Flights)
                .SelectMany(f => f.Segments)
                .OrderBy(s => s.DepartureTime)
                .ToList();

            for (int i = 1; i < flightSegments.Count; i++)
            {
                var previous = flightSegments[i - 1];
                var current = flightSegments[i];

                if (current.DepartureTime < previous.ArrivalTime)
                    throw new FlightSegmentDatesOverlapException(previous, current);
            }
        }

        private void EnsureStatus(TripStatus expectedStatus)
        {
            if (Status != expectedStatus)
            {
                throw new UnexpectedTripStatusException(Status, expectedStatus);
            }
        }
    }
}
