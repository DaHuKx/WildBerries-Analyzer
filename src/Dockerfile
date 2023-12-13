#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WildBerriesAnalyzer.ConsoleTest/WildBerriesAnalyzer.ConsoleTest.csproj", "WildBerriesAnalyzer.ConsoleTest/"]
COPY ["WildBerriesAnalyzer.Business/WildBerriesAnalyzer.Business.csproj", "WildBerriesAnalyzer.Business/"]
COPY ["WildBerriesAnalyzer.Data/WildBerriesAnalyzer.Data.csproj", "WildBerriesAnalyzer.Data/"]
COPY ["WildBerriesAnalyzer.Domain/WildBerriesAnalyzer.Domain.csproj", "WildBerriesAnalyzer.Domain/"]
RUN dotnet restore "WildBerriesAnalyzer.ConsoleTest/WildBerriesAnalyzer.ConsoleTest.csproj"
COPY . .
WORKDIR "/src/WildBerriesAnalyzer.ConsoleTest"
RUN dotnet build "WildBerriesAnalyzer.ConsoleTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WildBerriesAnalyzer.ConsoleTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WildBerriesAnalyzer.ConsoleTest.dll"]