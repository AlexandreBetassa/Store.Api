FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api/Store.User.Api.csproj", "Api/"]
COPY ["Application/Store.User.Application.csproj", "Application/"]
COPY ["CrossCutting/Store.User.Infrastructure.CrossCutting.csproj", "CrossCutting/"]
COPY ["Core/Store.Framework.Core.csproj", "Core/"]
COPY ["Domain/Store.User.Domain.csproj", "Domain/"]
COPY ["Infrastructure/Store.User.Infrastructure.Data.csproj", "Infrastructure/"]
RUN dotnet restore "./Api/Store.User.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "./Store.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Store.User.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.User.Api.dll"]