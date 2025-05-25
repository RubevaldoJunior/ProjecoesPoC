using ProjecoesPoC.Models;

namespace ProjecoesPoC.Utils
{
    public interface IDataReader
    {
        List<InputData> ReadData(string filePath);
    }
}
