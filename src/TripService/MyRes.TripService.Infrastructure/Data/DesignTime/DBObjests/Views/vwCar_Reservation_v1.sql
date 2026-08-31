CREATE OR ALTER VIEW dbo.vwCar_Reservation
AS
SELECT t.Id AS TripId,
       t.TripNo,
       t.CreatedAt,
       ti.Id AS TripItemId,
       cr.CarBrand,
       cr.CarModel,
       cr.PickupDate,
       cr.ReturnDate
FROM   dbo.Trip AS t
       INNER JOIN
       dbo.TripItem AS ti
       ON t.Id = ti.TripId
       INNER JOIN
       dbo.CarReservation AS cr
       ON ti.Id = cr.Id;