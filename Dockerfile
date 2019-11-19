﻿FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base

EXPOSE 3000

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src


# Copy csproj and restore as distinct layers
COPY Stack.GraphQL/Stack.GraphQL.csproj GraphQL/

COPY NuGet.config GraphQL/

RUN dotnet restore GraphQL/Stack.GraphQL.csproj --configfile ./GraphQL/NuGet.config

# Copy everything else and build
COPY ./Stack.GraphQL/ GraphQL/

WORKDIR "/src/GraphQL"
RUN dotnet build "Stack.GraphQL.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Stack.GraphQL.csproj" -c Release -o /app

FROM base AS final

WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Stack.GraphQL.dll"]
