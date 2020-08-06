#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SSO-BASE-NOVO.csproj", ""]
RUN dotnet restore "./SSO-BASE-NOVO.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SSO-BASE-NOVO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SSO-BASE-NOVO.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSO-BASE-NOVO.dll"]