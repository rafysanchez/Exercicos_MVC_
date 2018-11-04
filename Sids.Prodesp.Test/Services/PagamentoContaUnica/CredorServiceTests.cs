using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass()]
    public class CredorServiceTests
    {

        [TestMethod()]
        public void SalvarTest()
        {
            var credores = CreateCredoresFactory();

            Parallel.ForEach(credores, App.CredorService.Salvar);
            var credoresSalvos = ListarTodos();
            Assert.IsTrue(credoresSalvos.Any());
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            App.CredorService.Excluir(0);
            var credoresSalvos = ListarTodos();
            Assert.IsFalse(credoresSalvos.Any());
        }


        [TestMethod()]
        public void AtualizarcCredorTest()
        {
            App.CredorService.AtualizarcCredor(0);
            var credores = ListarTodos();
            Assert.IsNotNull(credores);
        }

        [TestMethod()]
        public void ListarTest()
        {
            var credoresSalvos = ListarTodos();
            Assert.IsFalse(credoresSalvos.Any());
        }
        private List<Credor> ListarTodos()
        {
            return App.CredorService.Listar(new Credor()).ToList();
        }

        private static List<Credor> CreateCredoresFactory()
        {
            List<Credor> credores = new List<Credor>
            {
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
                new Credor
                {
                    NomeReduzidoCredor = "Teste",
                    BaseCalculo = true,
                    Conveniado = false,
                    CpfCnpjUgCredor = "123456789",
                    Prefeitura = "Teste teste"
                },
            };
            return credores;
        }
    }
}