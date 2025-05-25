# ProjecoesPoC

---

## 📝 Visão Geral

`ProjecoesPoC` é uma prova de conceito (PoC) desenvolvida em .NET 9.0, focada na manipulação e projeção de dados. O projeto integra funcionalidades de persistência de dados utilizando **Entity Framework Core com Npgsql** para interagir com bases de dados PostgreSQL, e oferece capacidades de exportação de dados para arquivos Excel (`.xlsx`) através da biblioteca **ClosedXML**.

A aplicação é configurável via `appsettings.json`, permitindo uma gestão flexível dos parâmetros. Para facilitar a implantação e garantir um ambiente de execução consistente, o projeto é conteinerizado utilizando **Docker** e gerenciado de forma eficiente com **Docker Compose**.

## ✨ Funcionalidades Principais

* **Manipulação e Projeção de Dados:** Processa e transforma conjuntos de dados, aplicando lógicas de projeção específicas.
* **Integração com PostgreSQL:** Gerencia o acesso e a persistência de dados em bases de dados PostgreSQL através do Entity Framework Core.
* **Geração de Relatórios Excel:** Exporta dados processados para arquivos `.xlsx` formatados, utilizando a biblioteca ClosedXML.
* **Configuração Dinâmica:** Todos os parâmetros de conexão e configurações da aplicação são gerenciados via `appsettings.json` ou variáveis de ambiente.
* **Conteinerização Docker:** Empacota a aplicação e suas dependências em um contêiner Docker, assegurando portabilidade e isolamento.
* **Orquestração com Docker Compose:** Gerencia facilmente a aplicação e seu banco de dados em um ambiente isolado e replicável.

## 🚀 Como Executar

### Pré-requisitos

Para executar este projeto, você precisará ter instalado:

* [Docker Desktop](https://www.docker.com/products/docker-desktop) (ou uma alternativa para Docker, com Docker Compose incluído)

### 🐳 Executando com Docker Compose

O Docker Compose é a forma preferencial de executar este projeto, pois ele orquestra tanto a aplicação quanto o banco de dados PostgreSQL.

1.  **Navegue até o diretório raiz do projeto:**
    Abra seu terminal e vá para a pasta onde o seu arquivo `docker-compose.yml` e a pasta `ProjecoesPoC` estão localizados.
    ```bash
    cd /caminho/para/ProjecoesPoC/
    ```
2.  **Verifique e personalize as configurações (se necessário):**
    * **`docker-compose.yml`**: Este arquivo já contém as variáveis de ambiente (`POSTGRES_USER`, `POSTGRES_PASSWORD`, `POSTGRES_DB`) e a string de conexão (`DB_CONNECTION_STRING`) para a sua aplicação.
        * **Não é necessário modificar a string de conexão no `appsettings.json` para o ambiente Docker se você estiver usando a variável de ambiente `DB_CONNECTION_STRING` no `docker-compose.yml`.** A variável de ambiente sobrescreve a configuração do `appsettings.json`.
        * Certifique-se de que o `dockerfile: Dockerfile` esteja apontando para o seu `Dockerfile` correto. O `context: .` indica que o Dockerfile será procurado no diretório atual (onde o `docker-compose.yml` está).
    * **`Dockerfile`**: Verifique se a linha `COPY ProjecoesPoC/appsettings.json .` (e `appsettings.Development.json` se houver) está presente no estágio `final` para garantir que os arquivos de configuração base sejam copiados para a imagem.

3.  **Inicie os serviços com Docker Compose:**
    No diretório onde o `docker-compose.yml` está, execute:
    ```bash
    docker-compose up --build -d
    ```
    * `up`: Inicia os serviços definidos no `docker-compose.yml`.
    * `--build`: Garante que a imagem da aplicação seja reconstruída caso haja alterações no código-fonte ou no `Dockerfile`.
    * `-d`: (Detached mode) Inicia os contêineres em segundo plano, liberando o terminal.

    O Docker Compose irá:
    * Construir a imagem da sua aplicação (`population_app_poc`).
    * Puxar a imagem do PostgreSQL (`postgres:15`).
    * Criar e iniciar os contêineres (`postgres_db_container_poc` e `populacao_app_container_poc`).
    * Esperar até que o serviço de banco de dados (`postgres_db_poc`) esteja saudável antes de iniciar a aplicação (`population_app_poc`).

4.  **Verifique o status dos contêineres:**
    Você pode verificar o status dos seus serviços a qualquer momento:
    ```bash
    docker-compose ps
    ```
5.  **Parar os serviços:**
    Para parar e remover os contêineres, redes e volumes (exceto os volumes de dados persistentes):
    ```bash
    docker-compose down
    ```


---

## 🛠️ Tecnologias Utilizadas

Este projeto foi construído com as seguintes tecnologias e bibliotecas:

* **[.NET 9.0](https://dotnet.microsoft.com/)**
* **[Entity Framework Core 9.0.5](https://learn.microsoft.com/ef/core/)**: Framework ORM para acesso a dados.
* **[Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4](https://www.npgsql.org/)**: Provedor PostgreSQL para Entity Framework Core.
* **[ClosedXML 0.105.0](https://closedxml.github.io/)**: Biblioteca para criar e manipular arquivos Excel.
* **[Microsoft.Extensions.Configuration.Json 9.0.5](https://learn.microsoft.com/dotnet/api/microsoft.extensions.configuration.json)**: Suporte para arquivos de configuração JSON.
* **[Microsoft.Extensions.Hosting 9.0.5](https://learn.microsoft.com/dotnet/api/microsoft.extensions.hosting)**: Abstrações para hospedagem de aplicações.
* **[Docker](https://www.docker.com/)**: Plataforma para conteinerização de aplicações.
* **[Docker Compose](https://docs.docker.com/compose/)**: Ferramenta para definir e executar aplicações Docker multi-contêineres.

---

## 🤝 Contribuição

Contribuições são sempre bem-vindas! Se você encontrar um bug, tiver uma sugestão de melhoria ou quiser adicionar uma nova funcionalidade, sinta-se à vontade para:

1.  Abrir uma [Issue](https://github.com/seu-usuario/ProjecoesPoC/issues) para descrever o problema ou a ideia.
2.  Criar um [Pull Request](https://github.com/seu-usuario/ProjecoesPoC/pulls) com suas alterações.

Por favor, siga as boas práticas de desenvolvimento e inclua testes, se aplicável.

---

## 📜 Licença

Este projeto está licenciado sob a licença MIT. Para mais detalhes, consulte o arquivo [LICENSE](https://github.com/seu-usuario/ProjecoesPoC/blob/main/LICENSE) na raiz do repositório.

---

## 📧 Contato

Para quaisquer dúvidas, sugestões ou feedback, você pode entrar em contato:

* **[RubevaldoJunior]** - [rubevaldoj@gmail.com]

---
