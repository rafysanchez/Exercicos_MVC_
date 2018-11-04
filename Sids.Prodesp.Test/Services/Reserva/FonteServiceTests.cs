using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.Reserva
{
    [TestClass()]
    public class FonteServiceTests
    {
        [TestMethod()]
        public void PesquisarTest()
        {
            var fontes = App.FonteService.Buscar(new Fonte());
            Assert.IsNotNull(fontes);
        }

        
        [TestMethod()]
        public void InserirTest()
        {
            Fonte fontes = new Fonte {Codigo="171", Descricao="TesteUnitTwo"};
            AcaoEfetuada result = App.FonteService.Salvar(fontes, 0, 1);
            Assert.IsTrue(AcaoEfetuada.Sucesso == result);
        }

        [TestMethod()]
        public void EditarTest()
        {
            Fonte fontes = new Fonte { Id = 27 };
            fontes.Codigo = "1236";
            fontes.Descricao = "Teste Desc";
            AcaoEfetuada result = App.FonteService.Salvar(fontes, 0, 2);
            Assert.IsTrue(AcaoEfetuada.Sucesso == result);
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            Fonte fontes = new Fonte { Id = 30 };
            AcaoEfetuada result = App.FonteService.Excluir(fontes,0 ,3);
            Assert.IsTrue(AcaoEfetuada.Sucesso == result);
         }
    }
}