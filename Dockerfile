# filepath: /home/sobhi/MyProjects/CarRental/CarRental/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only project metadata first (for cacheable restore)
COPY CarRental.slnx ./
COPY src/CarRental.API/CarRental.API.csproj src/CarRental.API/
COPY src/CarRental.Application/CarRental.Application.csproj src/CarRental.Application/
COPY src/CarRental.Infrastructure/CarRental.Infrastructure.csproj src/CarRental.Infrastructure/
COPY src/CarRental.Domain/CarRental.Domain.csproj src/CarRental.Domain/

RUN dotnet restore src/CarRental.API/CarRental.API.csproj

# Then copy full source
COPY . .

RUN dotnet publish src/CarRental.API/CarRental.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarRental.API.dll"]