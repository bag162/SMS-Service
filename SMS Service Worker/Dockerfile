#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SMS Service Worker/SMSService.csproj", "SMS Service Worker/"]
COPY ["Backend.TaskMonitor/Backend.TaskMonitor.csproj", "Backend.TaskMonitor/"]
COPY ["Backend.Implemantation/Backend.Implemantation.csproj", "Backend.Implemantation/"]
COPY ["Backend.TarantoolDB/Backend.TarantoolDB.csproj", "Backend.TarantoolDB/"]
COPY ["Backend.DBInfrastructure/Backend.DBInfrastructure.csproj", "Backend.DBInfrastructure/"]
COPY ["Backend.Configuration/Backend.Configuration.csproj", "Backend.Configuration/"]
COPY ["Backend.Models.DTO/Backend.Models.csproj", "Backend.Models.DTO/"]
COPY ["Backend.CheckDBWorkers/Backend.CheckDBWorkers.csproj", "Backend.CheckDBWorkers/"]
COPY ["Backend.SMSWorker/Backend.SMSWorker.csproj", "Backend.SMSWorker/"]
RUN dotnet restore "SMS Service Worker/SMSService.csproj"
COPY . .
WORKDIR "/src/SMS Service Worker"
RUN dotnet build "SMSService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SMSService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMSService.dll"]