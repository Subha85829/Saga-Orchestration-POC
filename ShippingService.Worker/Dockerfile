FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["ShippingService.Worker/ShippingService.Worker.csproj", "ShippingService.Worker/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "ShippingService.Worker/ShippingService.Worker.csproj"

# Copy all source code
COPY . .
WORKDIR "/src/ShippingService.Worker"
RUN dotnet build "ShippingService.Worker.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ShippingService.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShippingService.Worker.dll"]