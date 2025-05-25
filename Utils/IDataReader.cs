using ProjecoesPoC.Models;
using System.Collections.Generic;

namespace ProjecoesPoC.Utils
{
    public interface IDataReader
    {
        List<InputData> ReadData(string filePath);
    }
}
