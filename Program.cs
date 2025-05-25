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
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;

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

                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null);
                    }));

                services.AddTransient<IDataReader, ExcelDataReader>();
                services.AddTransient<CruzamentoService>();
                services.AddTransient<ProcessadorDeDadosService>();
                services.AddHostedService<CruzamentoProcessadorService>();
            })
            .Build();

        await host.RunAsync();
    }
}

