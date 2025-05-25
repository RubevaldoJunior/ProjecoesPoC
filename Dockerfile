# Estágio 1: Build
# Usaremos o SDK do .NET 8.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copia o .csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código-fonte
COPY . ./
# Publica a aplicação (garantindo que appsettings.json seja copiado se configurado no .csproj)
RUN dotnet publish -c Release -o out --no-restore

# Estágio 2: Runtime
# Usaremos a imagem Runtime do .NET 8, que é menor.
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=build-env /app/out .

# Cria a pasta 'data' dentro do contêiner
RUN mkdir -p /app/data
# Copia o arquivo Excel para a pasta 'data' do contêiner
COPY ./Data/projecoes.xlsx /app/Data/projecoes.xlsx

COPY appsettings.json .

# Define o ponto de entrada (Verifique se 'PopulationPoc.dll' é o nome correto)
ENTRYPOINT ["dotnet", "ProjecoesPoC.dll"]