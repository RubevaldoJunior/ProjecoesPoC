ProjecoesPoC

🚀 Como Executar
Pré-requisitos
Para executar este projeto, você precisará ter instalado:

Docker Desktop (ou uma alternativa para Docker)

🐳 Executando com Docker
Navegue até o diretório raiz do projeto:
Abra seu terminal e vá para a pasta onde o Dockerfile e a pasta ProjecoesPoC estão localizados. Geralmente, é um nível acima da pasta ProjecoesPoC.

cd /caminho/para/ProjecoesPoC/

Configure o appsettings.json:
Abra o arquivo ProjecoesPoC/appsettings.json (e ProjecoesPoC/appsettings.Development.json se estiver usando) e ajuste as configurações, especialmente a string de conexão com o banco de dados PostgreSQL.

Exemplo de appsettings.json:

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

Importante: Se o seu banco de dados PostgreSQL também estiver em um contêiner Docker, use o nome do serviço Docker do PostgreSQL como Host na string de conexão (ex: Host=nome_do_servico_postgres).

Construa a imagem Docker:
No diretório raiz do projeto (onde o Dockerfile está), execute:

docker build -t projecoes_poc .

(Você pode substituir projecoes_poc pelo nome que preferir para sua imagem).

Execute o contêiner Docker:

docker run --name projecoes_poc_instancia -p 8080:80 projecoes_poc

--name projecoes_poc_instancia: Atribui um nome para a instância do seu contêiner.

-p 8080:80: Mapeia a porta 8080 do seu computador (host) para a porta 80 dentro do contêiner. Ajuste a porta do host (8080) se houver conflito ou se sua aplicação .NET expõe outra porta.

Se sua aplicação precisa se comunicar com outros contêineres (ex: um banco de dados), considere usar o Docker Compose para gerenciar múltiplos serviços em uma rede definida.

🛠️ Tecnologias Utilizadas
Este projeto foi construído com as seguintes tecnologias e bibliotecas:

.NET 9.0

Entity Framework Core 9.0.5: Framework ORM para acesso a dados.

Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4: Provedor PostgreSQL para Entity Framework Core.

ClosedXML 0.105.0: Biblioteca para criar e manipular arquivos Excel.

Microsoft.Extensions.Configuration.Json 9.0.5: Suporte para arquivos de configuração JSON.

Microsoft.Extensions.Hosting 9.0.5: Abstrações para hospedagem de aplicações.

Docker: Plataforma para desenvolver, enviar e executar aplicações em contêineres.

🤝 Contribuição
Contribuições são sempre bem-vindas! Se você encontrar um bug, tiver uma sugestão de melhoria ou quiser adicionar uma nova funcionalidade, sinta-se à vontade para:

Abrir uma Issue para descrever o problema ou a ideia.

Criar um Pull Request com suas alterações.

Por favor, siga as boas práticas de desenvolvimento e inclua testes, se aplicável.

📜 Licença
Este projeto está licenciado sob a licença MIT. Para mais detalhes, consulte o arquivo LICENSE na raiz do repositório.

📧 Contato
Para quaisquer dúvidas, sugestões ou feedback, você pode entrar em contato:

[RubevaldoJunior] - [rubevaldoj@gmail.com]
