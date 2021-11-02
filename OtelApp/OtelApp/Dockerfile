FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OtelApp.csproj", "OtelApp/"]
RUN dotnet restore "OtelApp/OtelApp.csproj"
COPY . "OtelApp/"
WORKDIR "/src/OtelApp"
RUN dotnet build "OtelApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OtelApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtelApp.dll"]
