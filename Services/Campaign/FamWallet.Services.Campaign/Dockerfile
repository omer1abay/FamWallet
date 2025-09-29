# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Campaign/FamWallet.Services.Campaign/FamWallet.Services.Campaign.csproj", "Services/Campaign/FamWallet.Services.Campaign/"]
COPY ["Shared/FamWallet.Shared/FamWallet.Shared.csproj", "Shared/FamWallet.Shared/"]
RUN dotnet restore "./Services/Campaign/FamWallet.Services.Campaign/FamWallet.Services.Campaign.csproj"
COPY . .
WORKDIR "/src/Services/Campaign/FamWallet.Services.Campaign"
RUN dotnet build "./FamWallet.Services.Campaign.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FamWallet.Services.Campaign.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamWallet.Services.Campaign.dll"]