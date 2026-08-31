-- v1

CREATE OR ALTER PROCEDURE [dbo].[uspGetCarReservationsByTripId]
    @TripId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
      TripId,
      TripNo,
      TripItemId,
      CarBrand,
      CarModel,
      PickupDate,
      ReturnDate
    FROM dbo.vwCar_Reservation
    WHERE TripId = @TripId;
END
