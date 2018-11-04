using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.Reserva
{
    [TestClass()]
    public class ProgramaServiceTests
    {

        [TestMethod()]
        public void SalvarTest()
        {
            var programa = new Programa { Ano = 2016, Cfp = "1235469874563", Descricao = "TesteUnitario", Ptres = "123456" };
            int sucesso = (int)App.ProgramaService.Salvar(programa,0,1);
            Assert.AreEqual(sucesso , 1);
        }


        [TestMethod()]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void GerarEstruturaAnoAtualTest()
        {
            App.ProgramaService.GerarEstruturaAnoAtual(0,1);
            Assert.Fail();
        }


        [TestMethod()]
        public void SearchTest()
        {
            var programas = App.ProgramaService.Buscar(new Programa());
            Assert.IsNotNull(programas);
        }

        [TestMethod()]
        public void GetAnosProgramaTest()
        {
            var listAno = App.ProgramaService.GetAnosPrograma().ToList();
            Assert.IsTrue(listAno.Count() > 0);
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            var programa = App.ProgramaService.Buscar(new Programa { Ano = 2016, Cfp = "1235469874563", Descricao = "TesteUnitario", Ptres = "123456" }).FirstOrDefault();
            AcaoEfetuada sucesso = App.ProgramaService.Excluir(programa, 0, 1);
            Assert.AreEqual(sucesso, AcaoEfetuada.Sucesso);
        }
        
    }
}