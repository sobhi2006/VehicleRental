# filepath: /home/sobhi/MyProjects/CarRental/CarRental/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and restore
COPY . .
RUN dotnet restore /src/src/CarRental.API/CarRental.API.csproj

# Build and publish
RUN dotnet publish /src/src/CarRental.API/CarRental.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarRental.API.dll"]