#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
WORKDIR /src
COPY ["FoxConnTesteApp/FoxConnTesteApp.csproj", "FoxConnTesteApp/"]
RUN dotnet restore "FoxConnTesteApp/FoxConnTesteApp.csproj"
COPY . .
WORKDIR "/src/FoxConnTesteApp"
RUN dotnet build "FoxConnTesteApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoxConnTesteApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoxConnTesteApp.dll"]