FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5020

ENV ASPNETCORE_URLS=http://+:5020

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Events/WebAPI/WebAPI.csproj", "Services/Events/WebAPI/"]
COPY ["BuildingBlocks/Packages/MessagingContracts/MessagingContracts.1.0.0.nupkg", "BuildingBlocks/Packages/"]
RUN dotnet nuget add source ["BuildingBlocks/Packages/"] --name local
RUN dotnet restore "Services/Events/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Events/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
