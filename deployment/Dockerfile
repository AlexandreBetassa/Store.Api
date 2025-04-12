FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Api/Fatec.Store.User.Api.csproj", "Api/"]
COPY ["Application/Fatec.Store.User.Application.csproj", "Application/"]
COPY ["CrossCutting/Fatec.Store.User.Infrastructure.CrossCutting.csproj", "CrossCutting/"]
COPY ["Domain/Fatec.Store.User.Domain.csproj", "Domain/"]
COPY ["Infrastructure/Fatec.Store.User.Infrastructure.Data.csproj", "Infrastructure/"]
COPY ["nuget.config", "Api/"]

RUN dotnet restore --configfile ./Api/nuget.config "./Api/Fatec.Store.User.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "./Fatec.Store.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fatec.Store.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fatec.Store.User.Api.dll"]
