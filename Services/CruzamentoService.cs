using ProjecoesPoC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjecoesPoC.Services
{
    public class CruzamentoService
    {
        public List<CruzamentoSexoIdade> CruzamentoPorSexoIdade(List<InputData> dados)
        {
            return [.. dados
                .GroupBy(p => new { p.Sexo, p.Idade })
                .Select(grupo => new CruzamentoSexoIdade
                {
                    Sexo = grupo.Key.Sexo,
                    Idade = grupo.Key.Idade,
                    Total = grupo.Sum(p => p.Total)
                })
                .OrderBy(cru => cru.Idade).ThenBy(cru => cru.Sexo)];
        }

        public List<CruzamentoLocalIdade> CruzamentoPorLocalIdade(List<InputData> dados)
        {
            return [.. dados
                .GroupBy(p => new { p.Local, p.Idade })
                .Select(grupo => new CruzamentoLocalIdade
                {
                    Local = grupo.Key.Local,
                    Idade = grupo.Key.Idade,
                    Total = grupo.Sum(p => p.Total)
                })
                .OrderBy(cru => cru.Local).ThenBy(cru => cru.Idade)];
        }

        public List<CruzamentoSexoLocal> CruzamentoPorSexoLocal(List<InputData> dados)
        {
            return [.. dados
                .GroupBy(p => new { p.Sexo, p.Local })
                .Select(grupo => new CruzamentoSexoLocal
                {
                    Local = grupo.Key.Local,
                    Sexo = grupo.Key.Sexo,
                    Total = grupo.Sum(p => p.Total)
                })
                .OrderBy(cru => cru.Local).ThenBy(cru => cru.Sexo)];
        }
    }
}
