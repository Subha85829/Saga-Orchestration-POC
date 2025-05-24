FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["Saga.Orchestrator/Saga.Orchestrator.csproj", "Saga.Orchestrator/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
RUN dotnet restore "Saga.Orchestrator/Saga.Orchestrator.csproj"

# Copy all source code
COPY . .
WORKDIR "/src/Saga.Orchestrator"
RUN dotnet build "Saga.Orchestrator.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "Saga.Orchestrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Saga.Orchestrator.dll"]