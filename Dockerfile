FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app
COPY ./ServerMonitor.sln.sln ./

COPY ./ServerMonitor/ServerMonitor.csproj ./ServerMonitor/ServerMonitor.csproj
RUN dotnet restore ./ServerMonitor/ServerMonitor.csproj

COPY ./ServerMonitor ./ServerMonitor
RUN dotnet build ./ServerMonitor/ServerMonitor.csproj -c Release

RUN dotnet publish "./ServerMonitor/ServerMonitor.csproj" -c Release -o "../../app/out"

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS http://*:80
EXPOSE 80

ENTRYPOINT dotnet ServerMonitor.dll apiKey=$API_KEY