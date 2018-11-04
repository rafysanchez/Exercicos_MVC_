using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Test.Services.Seguranca
{
    [TestClass()]
    public class MenuServiceTests
    {
        [TestMethod()]
        public void BuscarMenuTest()
        {
            var menus = App.MenuService.Buscar(new Menu());
            
            Assert.IsNotNull(menus);
        }
    }
}