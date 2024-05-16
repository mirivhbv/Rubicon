FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Keep this explicit COPY lines in order across all Dockerfiles
# to speed up docker build cache.
COPY ["Rubicon.sln", "Rubicon.sln"]
COPY ["Rubicon.Core/Rubicon.Core.csproj", "Rubicon.Core/"]
COPY ["Rubicon/Rubicon.csproj", "Rubicon/"]
COPY ["Rubicon.Tests/Rubicon.Tests.csproj", "Rubicon.Tests/"]
COPY "scripts" "scripts"
COPY docker-compose.dcproj ./
RUN dotnet restore "Rubicon.sln"

COPY . .
WORKDIR "/src/Rubicon"
RUN dotnet publish --no-restore -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Run migration
WORKDIR "/src/scripts"
RUN chmod +x generate-migration-scripts.sh
RUN /bin/bash generate-migration-scripts.sh '/src/Rubicon/Rubicon.csproj' 'rubicon-api.sql'

FROM build AS publish

FROM base AS final
# Do not move it to separate script to keep docker handle caching.
# Hack to make postgresql-client install work on slim
RUN mkdir -p /usr/share/man/man1 \
    && mkdir -p /usr/share/man/man7
RUN apt-get update \
	&& apt-get install -y --no-install-recommends postgresql-client \
	&& apt-get install libgdiplus -y \
	&& rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/scripts/run-service.sh ./run-service.sh
COPY --from=build /sql/rubicon-api.sql ./rubicon-api.sql
RUN chmod +x ./run-service.sh
ENTRYPOINT [ "/bin/bash", "./run-service.sh", "rubicondb", "mysecretpassword", "Rubicon.dll", "./rubicon-api.sql"]
# ENTRYPOINT ["dotnet", "Rubicon.dll"]