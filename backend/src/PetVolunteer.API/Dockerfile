FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetVolunteer.API/PetVolunteer.API.csproj", "src/PetVolunteer.API/"]
COPY ["src/PetVolunteer.Domain/PetVolunteer.Domain.csproj", "src/PetVolunteer.Domain/"]
COPY ["src/PetVolunteer.Application/PetVolunteer.Application.csproj", "src/PetVolunteer.Application/"]
COPY ["src/PetVolunteer.Infrastructure/PetVolunteer.Infrastructure.csproj", "src/PetVolunteer.Infrastructure/"]
RUN dotnet restore "src/PetVolunteer.API/PetVolunteer.API.csproj"
COPY . .
WORKDIR "/src/src/PetVolunteer.API"
RUN dotnet build "PetVolunteer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetVolunteer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetVolunteer.API.dll"]
