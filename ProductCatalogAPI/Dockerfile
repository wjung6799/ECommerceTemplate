FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProductCatalogAPI/ProductCatalogAPI.csproj", "ProductCatalogAPI/"]
RUN dotnet restore "ProductCatalogAPI/ProductCatalogAPI.csproj"
COPY . .
WORKDIR "/src/ProductCatalogAPI"
RUN dotnet build "ProductCatalogAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductCatalogAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductCatalogAPI.dll"]