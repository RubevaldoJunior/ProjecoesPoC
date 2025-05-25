# ProjecoesPoC

---

## 📝 Visão Geral

`ProjecoesPoC` é uma prova de conceito (PoC) desenvolvida em .NET 9.0, focada na manipulação e projeção de dados. O projeto integra funcionalidades de persistência de dados utilizando **Entity Framework Core com Npgsql** para interagir com bases de dados PostgreSQL, e oferece capacidades de exportação de dados para arquivos Excel (`.xlsx`) através da biblioteca **ClosedXML**.

A aplicação é configurável via `appsettings.json`, permitindo uma gestão flexível dos parâmetros. Para facilitar a implantação e garantir um ambiente de execução consistente, o projeto é conteinerizado utilizando **Docker**.

## ✨ Funcionalidades Principais

* **Manipulação e Projeção de Dados:** Processa e transforma conjuntos de dados, aplicando lógicas de projeção específicas.
* **Integração com PostgreSQL:** Gerencia o acesso e a persistência de dados em bases de dados PostgreSQL através do Entity Framework Core.
* **Geração de Relatórios Excel:** Exporta dados processados para arquivos `.xlsx` formatados, utilizando a biblioteca ClosedXML.
* **Configuração Dinâmica:** Todos os parâmetros de conexão e configurações da aplicação são gerenciados via `appsettings.json`, permitindo fácil adaptação a diferentes ambientes.
* **Conteinerização Docker:** Empacota a aplicação e suas dependências em um contêiner Docker, assegurando portabilidade e isolamento.

## 🚀 Como Executar

### Pré-requisitos

Para executar este projeto, você precisará ter instalado:

* [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (ou uma alternativa para Docker)

### 🐳 Executando com Docker

1.  **Navegue até o diretório raiz do projeto:**
    Abra seu terminal e vá para a pasta onde o `Dockerfile` e a pasta `ProjecoesPoC` estão localizados. Geralmente, é um nível acima da pasta `ProjecoesPoC`.
    ```bash
    cd /caminho/para/ProjecoesPoC/
    ```
2.  **Configure o `appsettings.json`:**
    Abra o arquivo `ProjecoesPoC/appsettings.json` (e `ProjecoesPoC/appsettings.Development.json` se estiver usando) e ajuste as configurações, especialmente a **string de conexão com o banco de dados PostgreSQL**.

    Exemplo de `appsettings.json`:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=your_db_host;Port=5432;Database=your_db_name;Username=your_db_user;Password=your_db_password"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      }
    }
    ```
    * **Importante:** Se o seu banco de dados PostgreSQL também estiver em um contêiner Docker, use o nome do serviço Docker do PostgreSQL como `Host` na string de conexão (ex: `Host=nome_do_servico_postgres`).

3.  **Construa a imagem Docker:**
    No diretório raiz do projeto (onde o `Dockerfile` está), execute:
    ```bash
    docker build -t projecoes_poc .
    ```
    (Você pode substituir `projecoes_poc` pelo nome que preferir para sua imagem).

4.  **Execute o contêiner Docker:**
    ```bash
    docker run --name projecoes_poc_instancia -p 8080:80 projecoes_poc
    ```
    * `--name projecoes_poc_instancia`: Atribui um nome para a instância do seu contêiner.
    * `-p 8080:80`: Mapeia a porta 8080 do seu computador (host) para a porta 80 dentro do contêiner. Ajuste a porta do host (`8080`) se houver conflito ou se sua aplicação .NET expõe outra porta.
    * Se sua aplicação precisa se comunicar com outros contêineres (ex: um banco de dados), considere usar o [Docker Compose](https://docs.docker.com/compose/) para gerenciar múltiplos serviços em uma rede definida.


---

## 🛠️ Tecnologias Utilizadas

Este projeto foi construído com as seguintes tecnologias e bibliotecas:

* **[.NET 9.0](https://dotnet.microsoft.com/)**
* **[Entity Framework Core 9.0.5](https://learn.microsoft.com/ef/core/)**: Framework ORM para acesso a dados.
* **[Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4](https://www.npgsql.org/)**: Provedor PostgreSQL para Entity Framework Core.
* **[ClosedXML 0.105.0](https://closedxml.github.io/)**: Biblioteca para criar e manipular arquivos Excel.
* **[Microsoft.Extensions.Configuration.Json 9.0.5](https://learn.microsoft.com/dotnet/api/microsoft.extensions.configuration.json)**: Suporte para arquivos de configuração JSON.
* **[Microsoft.Extensions.Hosting 9.0.5](https://learn.microsoft.com/dotnet/api/microsoft.extensions.hosting)**: Abstrações para hospedagem de aplicações.
* **[Docker](https://www.docker.com/)**: Plataforma para desenvolver, enviar e executar aplicações em contêineres.

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

* **[RubevaldoJunior]** - [Rubevaldoj@gmail.com]

---
