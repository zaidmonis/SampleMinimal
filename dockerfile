# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY MySln.sln ./
COPY MyWebApi/MyWebApi.csproj MyWebApi/
COPY MyClassLib/MyClassLib.csproj MyClassLib/
COPY MyTests/MyTests.csproj MyTests/

# Restore dependencies
RUN dotnet restore MySln.sln

# Copy everything else and build
COPY . ./
WORKDIR /src/MyWebApi
