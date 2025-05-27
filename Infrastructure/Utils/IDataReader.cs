using ProjecoesPoC.Domain.Models;
using System.Collections.Generic;

namespace ProjecoesPoC.Infrastructure.Utils
{
    public interface IDataReader
    {
        List<InputData> ReadData(string filePath);
    }
}
