using Domain.Entity;
using Domain.Repositories;
using System.Text.Json;

namespace Application.Repositories
{
    public class AluraRepository : IAluraRepository
    {
        public void SaveToTxtFile(DadosCursoAlura dadosCursoAlura)
        {
            string filePath = @$"C:\Users\{Environment.UserName}\Downloads\DadosDoCurso.txt";
            string jsonString = JsonSerializer.Serialize(dadosCursoAlura);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, jsonString);
            }
            else
            {
                File.AppendAllText(filePath, Environment.NewLine + jsonString);
            }
        }
    }
}