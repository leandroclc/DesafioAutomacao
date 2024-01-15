using Domain.Entity;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection();
            DependencyInjectionConfig.Configure(serviceProvider);

            var aluraService = serviceProvider.BuildServiceProvider().GetRequiredService<IAluraService>();
            var aluraRepository = serviceProvider.BuildServiceProvider().GetRequiredService<IAluraRepository>();

            DadosCursoAlura dadosCurso = aluraService.ScrapeAlura();
            aluraRepository.SaveToTxtFile(dadosCurso);
        }
    }
}