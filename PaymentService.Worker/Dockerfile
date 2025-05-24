FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["PaymentService.Worker/PaymentService.Worker.csproj", "PaymentService.Worker/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "PaymentService.Worker/PaymentService.Worker.csproj"

# Copy all source code
COPY . .
WORKDIR "/src/PaymentService.Worker"
RUN dotnet build "PaymentService.Worker.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "PaymentService.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.Worker.dll"]