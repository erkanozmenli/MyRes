
\MyRes\src\TravelerService>

migration command:
dotnet ef migrations add test2 --project MyRes.TravelerService.Infrastructure --startup-project MyRes.TravelerService.API --context TravelerDbContext --output-dir Data/DesignTime/Migrations

db update command:
dotnet ef database update --project MyRes.Travelers.Infrastructure --startup-project MyRes.Travelers.API --context TravelersDbContext

----------------------------------------------------------------------------------------------

\MyRes\src\TripService>

migration command:
dotnet ef migrations add test2 --project MyRes.TripService.Infrastructure --startup-project MyRes.TripService.API --context TripDbContext --output-dir Data/DesignTime/Migrations

db update command:
dotnet ef database update --project MyRes.TripService.Infrastructure --startup-project MyRes.TripService.API --context TripsDbContext


----------------------------------------------------------------------------------------------

1. DB objelerini versiyonlayarak migrate etmek için boş bir migration dosyası oluştur oluştur. (Güncellene bekleyen herhangi bir entity yoksa boş oluşur.)
dotnet ef migrations add test --project MyRes.TripService.Infrastructure --startup-project MyRes.TripService.API --context TripsDbContext --output-dir Data/DesignTime/Migrations

2. .sql dosyalarının Build Action'ını Embedded resource yap.

3. Oluşan migration dosyasındaki boş Up fonksiyonuna aşağıdaki kodları ekle. migrate etmek istediğin script'lerin adını aşağıda ver.

4. DB Update (Manuel veya Seed ile)

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

----------------------------------------------------------------------------------------------

 sqlcmd -S 127.0.0.1,30001 -U sa -P YourPassword12! -Q "SELECT @@VERSION"

 ----------------------------------------------------------------------------------------------

 dotnet add package Aspire.Hosting.AppHost
 dotnet remove package Aspire.Hosting.AppHost

 ----------------------------------------------------------------------------------------------

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
----------------------------------------------------------------------------------------------
Redis Connection String

Host: redis.dev.internal
Port: 6379
Password: ....
Username: leave it empty.
Security/Use TLS: true