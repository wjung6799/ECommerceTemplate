FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /app
EXPOSE 80

COPY . .
RUN dotnet restore ProductCatalogAPI.csproj

FROM build AS publish
RUN dotnet publish "ProductCatalogAPI.csproj" -c Release -o /app

WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProductCatalogAPI.dll"]
