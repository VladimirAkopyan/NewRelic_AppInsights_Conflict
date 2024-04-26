#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NewRelic_AppInsights_Conflict/NewRelic_AppInsights_Conflict.csproj", "NewRelic_AppInsights_Conflict/"]
RUN dotnet restore "./NewRelic_AppInsights_Conflict/NewRelic_AppInsights_Conflict.csproj"
COPY . .
WORKDIR "/src/NewRelic_AppInsights_Conflict"
RUN dotnet build "./NewRelic_AppInsights_Conflict.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NewRelic_AppInsights_Conflict.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewRelic_AppInsights_Conflict.dll"]