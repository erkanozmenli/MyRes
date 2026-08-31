# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/TripService/MyRes.TripService.Api/MyRes.TripService.Api.csproj", "src/TripService/MyRes.TripService.Api/"]
COPY ["src/TripService/MyRes.TripService.Application/MyRes.TripService.Application.csproj", "src/TripService/MyRes.TripService.Application/"]
COPY ["src/TripService/MyRes.TripService.Domain/MyRes.TripService.Domain.csproj", "src/TripService/MyRes.TripService.Domain/"]
COPY ["src/BuildingBlocks/MyRes.BuildingBlocks/MyRes.BuildingBlocks.csproj", "src/BuildingBlocks/MyRes.BuildingBlocks/"]
COPY ["src/TripService/MyRes.TripService.Infrastructure/MyRes.TripService.Infrastructure.csproj", "src/TripService/MyRes.TripService.Infrastructure/"]

COPY . .

RUN dotnet restore "src/TripService/MyRes.TripService.Api/MyRes.TripService.Api.csproj"
RUN dotnet build "src/TripService/MyRes.TripService.Api/MyRes.TripService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/TripService/MyRes.TripService.Api/MyRes.TripService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyRes.TripService.Api.dll"]