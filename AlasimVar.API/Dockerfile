#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AlasimVar.API/AlasimVar.API.csproj", "AlasimVar.API/"]
COPY ["AlasimVar.Infrastructure/AlasimVar.Infrastructure.csproj", "AlasimVar.Infrastructure/"]
COPY ["AlasimVar.Application/AlasimVar.Application.csproj", "AlasimVar.Application/"]
COPY ["AlasimVar.Domain/AlasimVar.Domain.csproj", "AlasimVar.Domain/"]
RUN dotnet restore "AlasimVar.API/AlasimVar.API.csproj"
COPY . .
WORKDIR "/src/AlasimVar.API"
RUN dotnet build "AlasimVar.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlasimVar.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlasimVar.API.dll"]
