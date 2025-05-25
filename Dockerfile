# Est�gio 1: Build
# Usaremos o SDK do .NET 8.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copia o .csproj e restaura as depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia todo o c�digo-fonte
COPY . ./
# Publica a aplica��o (garantindo que appsettings.json seja copiado se configurado no .csproj)
RUN dotnet publish -c Release -o out --no-restore

# Est�gio 2: Runtime
# Usaremos a imagem Runtime do .NET 8, que � menor.
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build-env /app/out .

# Cria a pasta 'data' dentro do cont�iner
RUN mkdir -p /app/data
# Copia o arquivo Excel para a pasta 'data' do cont�iner
COPY ./Data/projecoes.xlsx /app/Data/projecoes.xlsx

COPY appsettings.json .

# Define o ponto de entrada (Verifique se 'PopulationPoc.dll' � o nome correto)
ENTRYPOINT ["dotnet", "ProjecoesPoC.dll"]