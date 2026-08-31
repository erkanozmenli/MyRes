using MyRes.BuildingBlocks.Exceptions;

namespace MyRes.TripService.Domain.Exceptions
{
    public class RoundTripRequiresInboundException : DomainException
    {
        public RoundTripRequiresInboundException()
            : base(
                  "trip.flightreservation.roundtrip_requires_inbound",
                  "Round-trip must have one outbound and one inbound flight.")
        {

        }
    }
}
