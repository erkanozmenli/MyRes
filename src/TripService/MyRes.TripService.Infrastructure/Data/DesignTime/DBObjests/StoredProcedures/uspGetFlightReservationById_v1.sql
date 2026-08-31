-- v1

CREATE OR ALTER PROCEDURE [dbo].[uspGetFlightReservationById]
    @TripId UNIQUEIDENTIFIER,
    @Id INT
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
    ArrivalTime
    FROM vwFlight_Reservation
    WHERE TripId = @TripId AND TripItemId = @Id;

END
