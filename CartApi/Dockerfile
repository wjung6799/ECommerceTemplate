FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CartApi/CartApi.csproj", "CartApi/"]
RUN dotnet restore "CartApi/CartApi.csproj"
COPY . .
WORKDIR "/src/CartApi"
RUN dotnet build "CartApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CartApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CartApi.dll"]