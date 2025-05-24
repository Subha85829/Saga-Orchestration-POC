# Use official .NET 9 SDK image for build
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["InventoryService.Worker/InventoryService.Worker.csproj", "InventoryService.Worker/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "InventoryService.Worker/InventoryService.Worker.csproj"

# Copy all source code
COPY . .
WORKDIR "/src/InventoryService.Worker"
RUN dotnet build "InventoryService.Worker.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "InventoryService.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.Worker.dll"]

