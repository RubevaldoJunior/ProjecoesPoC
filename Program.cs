using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjecoesPoC.Data;
using ProjecoesPoC.Services;
using ProjecoesPoC.Utils;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Configura o Host para usar injeção de dependência e appsettings.json
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // Garante que o appsettings.json seja lido
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Obtém a configuração que foi lida
                var configuration = hostContext.Configuration;

                // Obtém a string de conexão do appsettings.json
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = configuration.GetConnectionString("PostgresDb");
                    Console.WriteLine("Usando connection string do appsettings.json.");
                }
                else
                {
                    Console.WriteLine("Usando connection string da variável de ambiente DB_CONNECTION_STRING.");
                }

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("String de conexão não encontrada. Defina a variável de ambiente DB_CONNECTION_STRING ou ConnectionStrings:PostgresDb em appsettings.json.");
                }

                Console.WriteLine($"String de Conexão Final: {connectionString}");

                // Configura o DbContext
                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                    }));

                // Registra os serviços
                services.AddTransient<IDataReader, ExcelDataReader>();
                services.AddTransient<CruzamentoService>();
                services.AddTransient<ProcessadorDeDadosService>();
                // Registra a classe principal E a IHostApplicationLifetime
                services.AddHostedService<CruzamentoProcessadorService>();
            })
            .Build();

        // Executa o host. O PopulationProcessorService será iniciado.
        await host.RunAsync();
    }
}

