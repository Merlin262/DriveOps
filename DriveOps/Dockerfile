FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia os arquivos necessários manualmente
COPY ../DriveOps.sln .

COPY ../DriveOps ./DriveOps
COPY ../DriveOps.Application ./DriveOps.Application
COPY ../DriveOps.Infrastructure ./DriveOps.Infrastructure
COPY ../DriveOps.ServiceDefaults ./DriveOps.ServiceDefaults
COPY ../DriveOps.Services ./DriveOps.Services
COPY ../DriveOps.Domain ./DriveOps.Domain

# Restaura e compila
WORKDIR /src/DriveOps
RUN dotnet restore
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/DriveOps
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DriveOps.Api.dll"]
