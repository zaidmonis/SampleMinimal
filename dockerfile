# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

# Copy csproj and restore
COPY ./MyWebApi/*.csproj ./MyWebApi/
WORKDIR /MyWebApi
RUN dotnet restore

# Copy everything else and build
COPY ./MyWebApi/. ./
RUN dotnet publish -c Release -o /app/out

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "MyApi.dll"]
