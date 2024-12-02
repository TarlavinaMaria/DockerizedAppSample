# Этап для среды выполнения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["DockerizedAppSample/DockerizedAppSample.csproj", "DockerizedAppSample/"]
RUN dotnet restore "DockerizedAppSample/DockerizedAppSample.csproj"
COPY . .
WORKDIR "/src/DockerizedAppSample"
RUN dotnet build "DockerizedAppSample.csproj" -c Release -o /app/build

# Этап публикации
FROM build AS publish
RUN dotnet publish "DockerizedAppSample.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный этап с применением миграций
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src /src

# Установка dotnet-ef
RUN dotnet tool install --global dotnet-ef && \
    export PATH="$PATH:/root/.dotnet/tools"

# Применение миграций и запуск приложения
ENTRYPOINT ["sh", "-c", "export PATH=$PATH:/root/.dotnet/tools && dotnet ef database update --project /src/DockerizedAppSample/DockerizedAppSample.csproj && dotnet DockerizedAppSample.dll"]
