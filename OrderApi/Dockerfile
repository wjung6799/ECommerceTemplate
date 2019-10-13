FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /app
EXPOSE 80

COPY . .
RUN dotnet restore OrderApi.csproj

FROM build AS publish
RUN dotnet publish "OrderApi.csproj" -c Release -o /app

WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "OrderApi.dll"]
