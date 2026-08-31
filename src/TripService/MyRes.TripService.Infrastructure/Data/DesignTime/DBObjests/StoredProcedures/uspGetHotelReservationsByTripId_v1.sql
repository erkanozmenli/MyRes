-- v1

CREATE OR ALTER PROCEDURE [dbo].[uspGetHotelReservationsByTripId]
    @TripId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
      TripId,
      TripNo,
      TripItemId,
      HotelName,
      CheckIn,
      CheckOut,
      Guests
    FROM dbo.vwHotel_Reservation
    WHERE TripId = @TripId;
END
