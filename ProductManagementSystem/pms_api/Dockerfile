#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductManagementSystem/pms_api/pms_api.csproj", "ProductManagementSystem/pms_api/"]
RUN dotnet restore "ProductManagementSystem/pms_api/pms_api.csproj"
COPY . .
WORKDIR "/src/ProductManagementSystem/pms_api"
RUN dotnet build "pms_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pms_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pms_api.dll"]