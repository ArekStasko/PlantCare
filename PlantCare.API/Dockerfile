FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PlantCare.API/PlantCare.API.csproj", "PlantCare.API/"]
COPY ["PlantCare.Commands/PlantCare.Commands.csproj", "PlantCare.Commands/"]
COPY ["PlantCare.Commands.Abstraction/PlantCare.Commands.Abstraction.csproj", "PlantCare.Commands.Abstraction/"]
COPY ["PlantCare.Domain/PlantCare.Domain.csproj", "PlantCare.Domain/"]
COPY ["PlantCare.Persistance/PlantCare.Persistance.csproj", "PlantCare.Persistance/"]
COPY ["PlantCare.Persistance.ReadDataManager/PlantCare.Persistance.ReadDataManager.csproj", "PlantCare.Persistance.ReadDataManager/"]
COPY ["PlantCare.Persistance.WriteDataManager/PlantCare.Persistance.WriteDataManager.csproj", "PlantCare.Persistance.WriteDataManager/"]
COPY ["PlantCare.Queries/PlantCare.Queries.csproj", "PlantCare.Queries/"]
COPY ["PlantCare.Queries.Abstraction/PlantCare.Queries.Abstraction.csproj", "PlantCare.Queries.Abstraction/"]
RUN dotnet restore "PlantCare.API/PlantCare.API.csproj"
COPY . .
WORKDIR "/src/PlantCare.API"
RUN dotnet build "PlantCare.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PlantCare.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlantCare.API.dll"]
