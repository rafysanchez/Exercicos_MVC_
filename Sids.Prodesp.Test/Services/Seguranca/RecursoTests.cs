using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Test.Services.Seguranca
{
    [TestClass()]
    public class RecursoTests
    {
        [TestMethod()]
        public void SalvarRecursoNovoTest()
        {
            var recurso = new Funcionalidade();
            var acao = new List<FuncionalidadeAcao>();
            recurso.Nome = "TesteSalvarLog";
            recurso.Publico = true;
            recurso.Status = true;
            recurso.Descricao = "Teste Salvar Log";
            recurso.URL = "Log/Index";
            var id = App.FuncionalidadeService.Salvar(recurso, acao, 1, 1);
            Assert.IsTrue(id>0);
        }
    }
}