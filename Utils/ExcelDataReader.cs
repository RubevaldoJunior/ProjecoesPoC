using ClosedXML.Excel;
using ProjecoesPoC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ProjecoesPoC.Utils
{
    public class ExcelDataReader : IDataReader
    {
        public List<InputData> ReadData(string filePath)
        {
            var dados = new List<InputData>();

            if(!File.Exists(filePath))
            {
                Console.WriteLine($"Erro: Arquivo Excel não encontrado em {filePath}");
                return dados;
            }

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null) { Console.WriteLine("Erro: Nenhuma planilha encontrada."); return dados; }

                    Console.WriteLine($"Lendo planilha: {worksheet.Name}");

                    int linhaComColunas = 6;
                    int linhaComDados = 7;

                    int idadeColuna = 1;
                    int sexoColuna = 2;
                    int localColuna = 5;
                    int anosColunas = 6;

                    var cabecalhoLinha = worksheet.Row(linhaComColunas);
                    var anos = new Dictionary<int, int>();

                    int colunaInicial = anosColunas;
                    while (true)
                    {
                        var celula = cabecalhoLinha.Cell(colunaInicial);
                        var celulaValor = celula.GetFormattedString();

                        int ano = 0;

                        if (celula.DataType == XLDataType.Number && celula.TryGetValue(out double anoDouble)) { ano = Convert.ToInt32(anoDouble); }
                        else if (!string.IsNullOrWhiteSpace(celulaValor) && int.TryParse(celulaValor.Trim(), out int AnoInt)) { ano = AnoInt; }

                        if (ano >= 1900 && ano <= 2100)
                        {
                            anos.Add(colunaInicial, ano);
                            colunaInicial++;
                        }
                        else break;

                        if (colunaInicial > 150) break;
                    }

                    if (!anos.Any()) { return dados; }

                    int anoFimColuna = anos.Keys.Max();

                    foreach (var linha in worksheet.RowsUsed().Skip(linhaComDados - 1))
                    {
                        try
                        {
                            var idadeStr = linha.Cell(idadeColuna).GetValue<string>();
                            var sexo = linha.Cell(sexoColuna).GetValue<string>();
                            var local = linha.Cell(localColuna).GetValue<string>();

                            if (string.IsNullOrWhiteSpace(idadeStr) || string.IsNullOrWhiteSpace(local) ||
                                string.IsNullOrWhiteSpace(sexo))
                            { continue; }

                            if (!int.TryParse(idadeStr.Trim(), out int idade)) { continue; }

                            string sexoCode = "";

                            if (sexo.Trim().StartsWith("M", StringComparison.OrdinalIgnoreCase))
                            {
                                sexoCode = "F";
                            }
                            else if (sexo.Trim().StartsWith("H", StringComparison.OrdinalIgnoreCase))
                            {
                                sexoCode = "M";
                            }
                            else if (sexo.Trim().StartsWith("A", StringComparison.OrdinalIgnoreCase))
                            {
                                sexoCode = "Ambos";
                            }

                            foreach (var anoInicial in anos)
                            {
                                int colunaIdx = anoInicial.Key;
                                int anoValor = anoInicial.Value;
                                var popCelula = linha.Cell(colunaIdx);
                                string popStr = popCelula.GetFormattedString();

                                if (!string.IsNullOrWhiteSpace(popStr))
                                {
                                    if (long.TryParse(popStr.Replace(".", "").Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out long total) && total >= 0)
                                    {
                                        dados.Add(new InputData
                                        {
                                            Idade = idade,
                                            Sexo = sexoCode,
                                            Local = local.Trim(),
                                            Ano = anoValor,
                                            Total = total
                                        });
                                    }
                                }
                            }
                        }
                        catch (Exception exRow) { Console.WriteLine($"ERRO ao processar a linha {linha.RowNumber()}: {exRow.Message}. Pulando linha."); }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"ERRO GERAL ao ler o arquivo Excel: {ex.Message}\n{ex.StackTrace}"); }

            Console.WriteLine($"Total de {dados.Count} registros processados do Excel.");
            return dados;
        }
    }
}
