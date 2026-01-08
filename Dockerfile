# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy csproj and restore dependencies
COPY *.sln .
COPY sugarbuddyAPI/*.csproj ./sugarbuddyAPI/
RUN dotnet restore

# Copy everything else and build
COPY sugarbuddyAPI/. ./sugarbuddyAPI/
WORKDIR /app/sugarbuddyAPI
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/sugarbuddyAPI/out .

# Expose port 8080 for Railway
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "sugarbuddyAPI.dll"]
