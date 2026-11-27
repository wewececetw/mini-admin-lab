FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY MiniAdmin.Core/MiniAdmin.Core.csproj MiniAdmin.Core/
COPY MiniAdmin.Api/MiniAdmin.Api.csproj MiniAdmin.Api/
RUN dotnet restore MiniAdmin.Api/MiniAdmin.Api.csproj

COPY MiniAdmin.Core/ MiniAdmin.Core/
COPY MiniAdmin.Api/ MiniAdmin.Api/
RUN dotnet publish MiniAdmin.Api/MiniAdmin.Api.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "MiniAdmin.Api.dll"]
