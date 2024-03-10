#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Santander.BestHackerNews.LiveManagerHost/Santander.BestHackerNews.LiveManagerHost.csproj", "Santander.BestHackerNews.LiveManagerHost/"]
COPY ["Santander.BestHackerNews.Application/Santander.BestHackerNews.Application.csproj", "Santander.BestHackerNews.Application/"]
COPY ["Santander.BestHackerNews.Domain/Santander.BestHackerNews.Domain.csproj", "Santander.BestHackerNews.Domain/"]
COPY ["Santander.BestHackerNews.Persistence/Santander.BestHackerNews.Persistence.csproj", "Santander.BestHackerNews.Persistence/"]
RUN dotnet restore "./Santander.BestHackerNews.LiveManagerHost/./Santander.BestHackerNews.LiveManagerHost.csproj"
COPY . .
WORKDIR "/src/Santander.BestHackerNews.LiveManagerHost"
RUN dotnet build "./Santander.BestHackerNews.LiveManagerHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Santander.BestHackerNews.LiveManagerHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Santander.BestHackerNews.LiveManagerHost.dll"]