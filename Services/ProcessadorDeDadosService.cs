using Microsoft.Extensions.Configuration;
using ProjecoesPoC.Data;
using ProjecoesPoC.Utils;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ProjecoesPoC.Services
{
    public class ProcessadorDeDadosService
    {
        private readonly IDataReader _dataReader;
        private readonly CruzamentoService _cruzamentoService;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration; // Para ler o caminho do arquivo

        public ProcessadorDeDadosService(IDataReader dataReader, CruzamentoService cruzamentoService, AppDbContext dbContext, IConfiguration configuration)
        {
            _dataReader = dataReader;
            _cruzamentoService = cruzamentoService;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task ProcessPopulationDataAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Verificando/Criando banco de dados (EnsureCreated)...");
            await _dbContext.Database.EnsureCreatedAsync(cancellationToken);
            Console.WriteLine("Banco de dados pronto.");

            var excelFilePath = _configuration.GetValue<string>("Settings:InputFilePath");
            if (string.IsNullOrEmpty(excelFilePath))
            {
                throw new InvalidOperationException("Caminho do arquivo 'InputFilePath' não encontrado em appsettings.json.");
            }

            var executionPath = Path.GetDirectoryName(AppContext.BaseDirectory);
            var fullPath = Path.Combine(executionPath ?? ".", excelFilePath);

            Console.WriteLine($"Tentando ler dados do Excel: {fullPath}");
            var populationRawData = _dataReader.ReadData(fullPath);

            if (populationRawData == null || !populationRawData.Any())
            {
                Console.WriteLine("Nenhum dado lido do Excel. Encerrando o processo.");
                return;
            }

            Console.WriteLine("Agregando Sexo X Local...");
            var sexoLocal= _cruzamentoService.CruzamentoPorSexoLocal(populationRawData);

            Console.WriteLine("Agregando Local X Idade...");
            var localIdade = _cruzamentoService.CruzamentoPorLocalIdade(populationRawData);

            Console.WriteLine("Agregando Sexo X Idade...");
            var sexoIdade = _cruzamentoService.CruzamentoPorSexoIdade(populationRawData);

            Console.WriteLine("Salvando agregações no banco de dados...");
            _dbContext.CruzamentoSexoLocals.RemoveRange(_dbContext.CruzamentoSexoLocals);
            _dbContext.CruzamentoLocalIdades.RemoveRange(_dbContext.CruzamentoLocalIdades);
            _dbContext.CruzamentoSexoIdades.RemoveRange(_dbContext.CruzamentoSexoIdades);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _dbContext.CruzamentoSexoLocals.AddRangeAsync(sexoLocal, cancellationToken);
            await _dbContext.CruzamentoLocalIdades.AddRangeAsync(localIdade, cancellationToken);
            await _dbContext.CruzamentoSexoIdades.AddRangeAsync(sexoIdade, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            Console.WriteLine("Processo concluído com sucesso!");
        }

    }
}
