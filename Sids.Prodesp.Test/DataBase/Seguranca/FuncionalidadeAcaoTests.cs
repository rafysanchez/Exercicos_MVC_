using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Test.DataBase.Seguranca
{
    [TestClass()]
    public class FuncionalidadeAcaoTests
    {
        [TestMethod()]
        public void BuscarFuncionalidadeAcaoTest()
        {
            var funcionalidadeAcoes = App.FuncionalidadeAcaoService.Buscar(new FuncionalidadeAcao());
            Assert.IsTrue(funcionalidadeAcoes.Count > 0);
        }
     }
}