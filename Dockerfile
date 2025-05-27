FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build-env /app/out .

RUN mkdir -p /app/data
COPY ./Infrastructure/Data/projecoes.xlsx /app/Infrastructure/Data/projecoes.xlsx

COPY appsettings.json .

ENTRYPOINT ["dotnet", "ProjecoesPoC.dll"]