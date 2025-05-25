using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjecoesPoC.Services
{
    public class CruzamentoProcessadorService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider; // Ainda necessário para criar o escopo
        private readonly IHostApplicationLifetime _appLifetime;

        public CruzamentoProcessadorService(IServiceProvider serviceProvider, IHostApplicationLifetime appLifetime)
        {
            _serviceProvider = serviceProvider;
            _appLifetime = appLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Iniciando processamento populacional...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var processor = scope.ServiceProvider.GetRequiredService<ProcessadorDeDadosService>();
                try
                {
                    await processor.ProcessPopulationDataAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERRO FATAL: {ex.Message}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                    Console.ResetColor();
                }
                finally
                {
                    _appLifetime.StopApplication();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Processamento finalizado.");
            return Task.CompletedTask;
        }
    }
}
