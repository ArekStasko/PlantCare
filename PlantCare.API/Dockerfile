FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PlantCare.API/PlantCare.API.csproj", "PlantCare.API/"]
RUN dotnet restore "PlantCare.API/PlantCare.API.csproj"
COPY . .
WORKDIR "/src/PlantCare.API"
RUN dotnet build "PlantCare.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlantCare.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlantCare.API.dll"]
