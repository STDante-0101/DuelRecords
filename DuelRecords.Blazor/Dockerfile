FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["DuelRecords.Blazor.csproj", "./"]
RUN dotnet restore "DuelRecords.Blazor.csproj"

COPY . .
RUN dotnet publish "DuelRecords.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "DuelRecords.Blazor.dll"]