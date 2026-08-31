-- v1

CREATE OR ALTER PROCEDURE [dbo].[uspGetFlightReservationsByTripId]
    @TripId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
    TripId,
    TripNo,
    TripItemId,
    FlightId,
    FlightSegmentId,
    PreviousSegmentId,
    TripType,
    Direction,
    FromAirport,
    ToAirport,
    DepartureTime,
    ArrivalTime,
    UserId
    FROM vwFlight_Reservation
    WHERE TripId = @TripId;

END
