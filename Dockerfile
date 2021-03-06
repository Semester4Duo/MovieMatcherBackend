﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MovieMatcher.csproj", "./"]
RUN dotnet restore "MovieMatcher.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "MovieMatcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieMatcher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNET_CORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "MovieMatcher.dll"]
