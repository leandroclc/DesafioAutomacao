using Domain.Entity;
using Domain.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace Application.Services
{
    public class AluraService : IAluraService
    {
        private readonly IWebDriver _driver;
        private readonly int SecondsToWait = 10;
        public AluraService()
        {
            _driver = new ChromeDriver();
        }
        public DadosCursoAlura ScrapeAlura()
        {
            NavigateToAluraWebsite();
            PerformSearch("RPA");
            SelectFilter();
            WaitPageToLoad();
            DadosCursoAlura dadosCurso = new();
            ObtainTitulondDesc(dadosCurso);
            ClickFirstItem();
            ObtainCargaAndProf(dadosCurso);
            Dispose();

            return dadosCurso;
        }
        private void NavigateToAluraWebsite()
        {
            _driver.Navigate().GoToUrl("https://www.alura.com.br/");
        }

        private void PerformSearch(string searchTerm)
        {
            try
            {
                IWebElement searchInput = _driver.FindElement(By.Id("header-barraBusca-form-campoBusca"));
                searchInput.SendKeys(searchTerm);

                IWebElement searchButton = _driver.FindElement(By.ClassName("header__nav--busca-submit"));
                searchButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar: {ex.Message}");
            }
        }

        private void SelectFilter()
        {
            try
            {
                IWebElement filterOptions = _driver.FindElement(By.XPath(@"//*[@id='busca-form']/span"));
                filterOptions.Click();
                IWebElement filterCursos = _driver.FindElement(By.XPath(@"//span[contains(@class, 'busca--filtro--nome-filtro') and contains(text(), 'Cursos')]"));
                filterCursos.Click();

                IWebElement searchButton = _driver.FindElement(By.Id("busca--filtrar-resultados"));
                searchButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Selecionar Filtro: {ex.Message}");
            }

        }
        private void WaitPageToLoad()
        {
            try
            {
                IWebElement firstItem = _driver.FindElement(By.XPath(@"//*[@id='busca-resultados']/ul/li[1]/a"));
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(SecondsToWait));
                wait.Until(d => firstItem.Displayed);
            }
            catch (Exception ex)
            {
                throw new Exception($"Curso não encontrado: {ex.Message}");
            }
        }
        private void ObtainTitulondDesc(DadosCursoAlura dadosCurso)
        {
            try
            {
                IWebElement titleElement = _driver.FindElement(By.XPath(@"//*[@id='busca-resultados']/ul/li[1]/a/div/h4"));
                IWebElement descricaoElement = _driver.FindElement(By.XPath(@"//*[@id='busca-resultados']/ul/li[1]/a/div/p"));

                dadosCurso.Titulo = titleElement.Text;
                dadosCurso.Descricao = descricaoElement.Text;
            }
            catch (Exception ex)
            {
                throw new Exception($"Título ou Descrição não encontrado: {ex.Message}");
            }

        }
        private void ClickFirstItem()
        {
            try
            {
                IWebElement firstItem = _driver.FindElement(By.XPath(@"//*[@id='busca-resultados']/ul/li[1]/a"));
                firstItem.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao clicar no item encontrado: {ex.Message}");
            }
        }

        private void ObtainCargaAndProf(DadosCursoAlura dadosCurso)
        {
            try
            {
                IWebElement instructorElement = _driver.FindElement(By.ClassName("instructor-title--name"));
                IWebElement cargaHorariaElement = _driver.FindElement(By.XPath(@"/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]"));
                dadosCurso.Professor = instructorElement.Text;
                dadosCurso.CargaHoraria = cargaHorariaElement.Text;
            }
            catch (Exception ex)
            {
                throw new Exception($"Carga Horária ou Professor não encontrado: {ex.Message}");
            }
        }
        private void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
