FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["WebMVC/WebMVC.csproj", "WebMVC/"]
RUN dotnet restore "WebMVC/WebMVC.csproj"
COPY . .
WORKDIR "/src/WebMVC"
RUN dotnet build "WebMVC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebMVC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebMVC.dll"]