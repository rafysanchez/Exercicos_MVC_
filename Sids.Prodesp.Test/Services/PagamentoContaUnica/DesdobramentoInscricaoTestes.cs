
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass]
    public class DesdobramentoInscricaoTestes
    {

        Desdobramento desdobramento = new Desdobramento();
              

        [TestMethod()]
        public void ConsultarDesdobramentoApoio()
        {
            desdobramento = CreateInstanceDesdobramento();
            var result = App.ProdespPagamentoContaUnicaService.DesdobramentoApoio(desdobramento, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

       

        private Desdobramento CreateInstanceDesdobramento()
        {
            Desdobramento model = new Desdobramento
            {
                NumeroDocumento = "179900001001",
                NumeroContrato = "",
                CodigoServico = "2001",
                ValorDistribuido = 1,
                DescricaoServico = "",
                DescricaoCredor = "",
                NomeReduzidoCredor = "",
                TipoDespesa = "",
                ValorIssqn = 0,
                ValorIr = 0,
                ValorInss = 0,
                AceitaCredor = true,
                DocumentoTipoId = 5,
                DesdobramentoTipoId =  1,

            };
            return model;
        }

    }
}
