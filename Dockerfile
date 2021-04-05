FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/domain/core/Eatigo.Eatilink.Api/Eatigo.Eatilink.Api.csproj", "src/domain/core/Eatigo.Eatilink.Api/"]
RUN dotnet restore "src/domain/core/Eatigo.Eatilink.Api/Eatigo.Eatilink.Api.csproj"
COPY . .
WORKDIR "/src/src/domain/core/Eatigo.Eatilink.Api"
RUN dotnet build "Eatigo.Eatilink.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Eatigo.Eatilink.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eatigo.Eatilink.Api.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Eatigo.Eatilink.Api.dll