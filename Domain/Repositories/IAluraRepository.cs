using Domain.Entity;

namespace Domain.Repositories
{
    public interface IAluraRepository
    {
        void SaveToTxtFile(DadosCursoAlura dadosCursoAlura);
    }
}
