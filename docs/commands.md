
\MyRes\src\TripService>

migration command:

```bash
dotnet ef migrations add test2 --project MyRes.TripService.Infrastructure --startup-project MyRes.TripService.API --context TripDbContext --output-dir Data/DesignTime/Migrations
```


db update command:

```bash
dotnet ef database update --project MyRes.TripService.Infrastructure --startup-project MyRes.TripService.API --context TripsDbContext
```

----------------------------------------------------------------------------------------------


## Database Migration Guide

1. **Create an empty migration file** to version and migrate database (DB) objects.  
    If there are no pending entity changes, an empty migration will be created.
    
    ```bash
    dotnet ef migrations add test --project MyRes.TripService.Infrastructure --startup-project MyRes.TripService.API --context TripsDbContext --output-dir Data/DesignTime/Migrations
    ```
    
2. **Set the Build Action of the `.sql` files to `Embedded Resource`.**
    
3. **Add the following code to the empty `Up` method** of the generated migration file.  
    Add the names of the scripts you want to migrate to the `scripts` array.
    
    ```csharp
    var scripts = new[]
    {
        "vwFlight_Reservation_v1.sql",
        "vwCar_Reservation_v1.sql",
        "vwHotel_Reservation_v1.sql",
        "uspGetFlightReservationsByTripId_v1.sql",
        "uspGetCarReservationsByTripId_v1.sql",
        "uspGetHotelReservationsByTripId_v1.sql",
        "uspGetFlightReservationById_v1.sql"
    };
    
    foreach (var file in scripts)
    {
        migrationBuilder.Sql(
            ResourceLoader.ReadFileAsString(GetType().Assembly, file));
    }
    ```
    
4. **Update the database** either manually or by running the seed process.

----------------------------------------------------------------------------------------------
```bash
sqlcmd -S 127.0.0.1,30001 -U sa -P YourPassword12! -Q "SELECT @@VERSION"
```
 ----------------------------------------------------------------------------------------------

```shell
 function Show-Tree {
    param(
        [string]$Path = ".",
        [int]$Depth = 2
    )

    tree $Path /A |
    ForEach-Object {
        $level = ([regex]::Matches($_, '\|   ')).Count
        if ($level -le $Depth) { $_ }
    }
}
```
----------------------------------------------------------------------------------------------

```text
Redis Connection String

Host: redis.dev.internal
Port: 6379
Password: ....
Username: leave it empty.
Security/Use TLS: true
```