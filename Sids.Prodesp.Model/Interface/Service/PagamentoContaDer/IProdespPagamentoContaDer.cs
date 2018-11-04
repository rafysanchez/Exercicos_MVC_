using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using System.Collections.Generic;

namespace Sids.Prodesp.Model.Interface.Service.PagamentoContaDer
{
    public interface IProdespPagamentoContaDer
    {
        object PreparacaoArquivoRemessa(string key, string password, ArquivoRemessa objModel, string impressora);

        object CancelamentoArquivoRemessa(string key, string password, ArquivoRemessa objModel, string impressora);

        object ImpressaoReemissaoRelacaoOD(string key, string password, ArquivoRemessa objModel, string impressora);

        object ConsultarArquivoTipoDataVenc2(string key, string password, ArquivoRemessa objModel, string impressora);
        

        object ConsultaArquivoPreparado(string key, string password, ArquivoRemessa objModel, string impressora);

        object ImpressaoRelacaoOD(string key, string password, PreparacaoPagamento objModel, string impressora);
        string ConsultaOP(string chave, string senha, string numeroDocumentoGerador);
        List<ConfirmacaoPagamento> ConsultaPagtosConfirmarSIDS(string key, string password, string empressora, ConfirmacaoPagamento entrada);
    }
}
