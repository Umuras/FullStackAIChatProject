# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Proje dosyasýný kopyala ve restore et
COPY ["backend.csproj", "."]
RUN dotnet restore "./backend.csproj"

# Config dosyalarýný kopyala (Production ve Development)
COPY ["appsettings.Production.json", "./"]
COPY ["appsettings.Development.json", "./"]

# Tüm kaynak kodu kopyala
COPY . .

# Build iþlemi
RUN dotnet build "./backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app

# Publish edilmiþ dosyalarý kopyala
COPY --from=publish /app/publish .

# Container giriþ noktasý
ENTRYPOINT ["dotnet", "backend.dll"]
