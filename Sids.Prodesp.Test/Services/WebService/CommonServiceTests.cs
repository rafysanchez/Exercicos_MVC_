using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Core.Services.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Core.Services.WebService.Tests
{
    [TestClass()]
    public class CommonServiceTests
    {
        [TestMethod()]
        public void CancelamentoOpApoioTest()
        {
            var result = App.CommonService.CancelamentoOpApoio(new ProgramacaoDesembolso { NumeroDocumento = "179900001/001", DocumentoTipoId = 5 });
            Assert.IsTrue(result.Any());
        }

        [TestMethod()]
        public void BloqueioOpApoioTest()
        {
            var result = App.CommonService.BloqueioOpApoio(new ProgramacaoDesembolso { NumeroDocumento = "179900001/001", DocumentoTipoId = 5 });
            Assert.IsTrue(result.Any());
        }

        [TestMethod()]
        public void ConsultaPDTest()
        {
            var result = App.CommonService.ConsultaPD(new Usuario(), "162101","16055", "2017PD00013");
            Assert.IsNotNull(result);
        }
    }
}