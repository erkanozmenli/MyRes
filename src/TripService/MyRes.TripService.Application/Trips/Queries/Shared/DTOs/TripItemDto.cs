using System.Text.Json.Serialization;

namespace MyRes.TripService.Application.Trips.Queries.Shared.DTOs
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(FlightReservationDto), typeDiscriminator: "flightReservation")]
    public abstract record TripItemDto
    {
        public int Id { get; init; }
    }
}
