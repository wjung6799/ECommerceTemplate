FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /app
EXPOSE 80

COPY . .
RUN dotnet restore CartApi.csproj

FROM build AS publish
RUN dotnet publish "CartApi.csproj" -c Release -o /app

WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CartApi.dll"]
