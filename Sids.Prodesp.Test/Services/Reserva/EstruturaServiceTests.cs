using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.Reserva
{
    [TestClass()]
    public class EstruturaServiceTests
    {
        [TestMethod()]
        public void BuscarEstruturaTest()
        {
            List<Estrutura> estruturas = App.EstruturaService.Buscar(new Estrutura()).ToList();
            Assert.IsNotNull(estruturas);
        }

        [TestMethod()]
        public void InserirEstruturaTest()
        {
            Estrutura estrutura = new Estrutura { Programa = 1, Nomenclatura = "Teste" };
            AcaoEfetuada acao = App.EstruturaService.Salvar(estrutura, 0, 1);
            Assert.IsTrue(AcaoEfetuada.Sucesso == acao);
        }

        [TestMethod()]
        public void AlterarEstruturaTest()
        {

            Estrutura estrutura = App.EstruturaService.Buscar(new Estrutura { Programa = 1, Nomenclatura = "Teste" }).FirstOrDefault();
            estrutura.Nomenclatura = "Teste2";
            AcaoEfetuada acao = App.EstruturaService.Salvar(estrutura, 0, 2);
            Assert.IsTrue(AcaoEfetuada.Sucesso == acao);
        }

        [TestMethod()]
        public void ExcluirEstruturaTest()
        {
            Estrutura estrutura = App.EstruturaService.Buscar(new Estrutura { Programa = 1, Nomenclatura = "Teste2" }).FirstOrDefault();
            AcaoEfetuada acao = App.EstruturaService.Excluir(estrutura, 0, 3);
            Assert.IsTrue(AcaoEfetuada.Sucesso == acao);
        }

        [TestMethod()]
        public void ObterEstruturaPorProgramaTest()
        {
            var estruturas = App.EstruturaService.ObterPorPrograma(new Programa {Codigo = 1});
            Assert.IsNotNull(estruturas);
        }
    }
}