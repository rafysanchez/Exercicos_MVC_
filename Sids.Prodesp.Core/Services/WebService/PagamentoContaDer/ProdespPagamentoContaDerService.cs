using System.Linq;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using Sids.Prodesp.Infrastructure.Services.Seguranca;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.Core.Services.WebService.PagamentoContaDer
{
    public class ProdespPagamentoContaDerService : BaseService
    {
        private readonly IProdespPagamentoContaDer _contaDer;
        private static RegionalService _regional;
        private static UsuarioService _uService;

        public ProdespPagamentoContaDerService(ILogError logError, IProdespPagamentoContaDer contaDer) : base(logError)
        {
            _contaDer = contaDer;
            _uService = _uService ?? new UsuarioService(logError, new UsuarioDal(), new PerfilUsuarioDal(), new PerfilDal(), new SiafemSegurancaWs(), new RegionalDal());
            _regional = _regional?? new RegionalService(logError, new RegionalDal());
        }

        public object PreparacaoArquivoRemessa(ArquivoRemessa entity, string key, string password)
        {
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();

            return _contaDer.PreparacaoArquivoRemessa(key, password, entity, usuario?.Impressora132);
        }



        public object ImpressaoReemissaoRelacaoOD(ArquivoRemessa entity, string key, string password)
        {
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();

            return _contaDer.ImpressaoReemissaoRelacaoOD(key, password, entity, usuario?.Impressora132);
        }

        public object ImpressaoRelacaoOD(PreparacaoPagamento entity, string key, string password)
        {
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();

            return _contaDer.ImpressaoRelacaoOD(key, password, entity, usuario?.Impressora132);
        }

        public object CancelamentoArquivoRemessa(ArquivoRemessa entity, string key, string password)
        {
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();

            return _contaDer.CancelamentoArquivoRemessa(key, password, entity, usuario?.Impressora132);
        }


        public string ConsultaOP(string chave, string senha, string numeroDocumentoGerador)
        {
            return _contaDer.ConsultaOP(chave, senha, numeroDocumentoGerador);
        }


        public object ConsultaArquivoPreparado(ArquivoRemessa entity, string key, string password)
        {
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();

            return _contaDer.ConsultaArquivoPreparado(key, password, entity, usuario?.Impressora132);
        }

        public object ConsultarArquivoTipoDespesaDataVenc2(ArquivoRemessa arquivoRemessa, string key, string password)
        {
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();

            return _contaDer.ConsultarArquivoTipoDataVenc2(key, password, arquivoRemessa, usuario?.Impressora132);
        }


    }
}
