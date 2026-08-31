# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/NotificationService/MyRes.NotificationService.Api/MyRes.NotificationService.Api.csproj", "src/NotificationService/MyRes.NotificationService.Api/"]
COPY ["src/BuildingBlocks/MyRes.BuildingBlocks/MyRes.BuildingBlocks.csproj", "src/BuildingBlocks/MyRes.BuildingBlocks/"]
COPY ["src/NotificationService/MyRes.NotificationService.Application/MyRes.NotificationService.Application.csproj", "src/NotificationService/MyRes.NotificationService.Application/"]
COPY ["src/NotificationService/MyRes.NotificationService.Domain/MyRes.NotificationService.Domain.csproj", "src/NotificationService/MyRes.NotificationService.Domain/"]
COPY ["src/NotificationService/MyRes.NotificationService.Infrastructure/MyRes.NotificationService.Infrastructure.csproj", "src/NotificationService/MyRes.NotificationService.Infrastructure/"]

COPY . .

RUN dotnet restore "src/NotificationService/MyRes.NotificationService.Api/MyRes.NotificationService.Api.csproj"
RUN dotnet build "src/NotificationService/MyRes.NotificationService.Api/MyRes.NotificationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/NotificationService/MyRes.NotificationService.Api/MyRes.NotificationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyRes.NotificationService.Api.dll"]