CREATE OR ALTER VIEW dbo.vwFlight_Reservation
AS
SELECT   t.Id AS TripId,
         t.TripNo,
         t.UserId,
         ti.Id AS TripItemId,
         f.Id AS FlightId,
         fs.Id AS FlightSegmentId,
         fs.PreviousSegmentId,
         fr.TripType,
         f.Direction,
         fs.FromAirport,
         fs.ToAirport,
         fs.DepartureTime,
         fs.ArrivalTime
FROM     dbo.Trip AS t
         INNER JOIN
         dbo.TripItem AS ti
         ON t.Id = ti.TripId
         INNER JOIN
         dbo.FlightReservation AS fr
         ON ti.Id = fr.Id
         INNER JOIN
         dbo.Flight AS f
         ON fr.Id = f.TripItemId
         LEFT OUTER JOIN
         dbo.FlightSegment AS fs
         ON f.Id = fs.FlightId