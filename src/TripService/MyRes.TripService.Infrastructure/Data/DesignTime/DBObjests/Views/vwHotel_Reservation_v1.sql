CREATE OR ALTER VIEW dbo.vwHotel_Reservation
AS
SELECT t.Id AS TripId,
       t.TripNo,
       t.CreatedAt,
       ti.Id AS TripItemId,
       hr.HotelName,
       hr.CheckIn,
       hr.CheckOut,
       hr.Guests
FROM   dbo.Trip AS t
       INNER JOIN
       dbo.TripItem AS ti
       ON t.Id = ti.TripId
       INNER JOIN
       dbo.HotelReservation AS hr
       ON ti.Id = hr.Id;