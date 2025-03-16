FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/Autenticacao.Jwt.Api.csproj", "src/Api/"]
COPY ["src/Application/Autenticacao.Jwt.Application.csproj", "src/Application/"]
COPY ["src/CrossCutting/Autenticacao.Jwt.CrossCutting.csproj", "src/CrossCutting/"]
COPY ["src/Domain/Autenticacao.Jwt.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Autenticacao.Jwt.Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "./src/Api/Autenticacao.Jwt.Api.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "./Autenticacao.Jwt.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Autenticacao.Jwt.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autenticacao.Jwt.Api.dll"]