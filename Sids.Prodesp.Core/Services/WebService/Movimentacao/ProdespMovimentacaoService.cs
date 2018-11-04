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
using Sids.Prodesp.Model.Interface.Service.Movimentacao;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Entity.Configuracao;
using System.Collections.Generic;

namespace Sids.Prodesp.Core.Services.WebService.PagamentoContaDer
{
    public class ProdespMovimentacaoService : BaseService
    {
        private readonly IProdespMovimentacao _contaDer;
        private static UsuarioService _uService;

        public ProdespMovimentacaoService(ILogError logError, IProdespMovimentacao contaDer) : base(logError)
        {
            _contaDer = contaDer;
            _uService = _uService ?? new UsuarioService(logError, new UsuarioDal(), new PerfilUsuarioDal(), new PerfilDal(), new SiafemSegurancaWs(), new RegionalDal());
        }
        
        public object PreparacaoMovimentacaoInterna(string key, string password,Programa programa, Estrutura estrutura, List<MovimentacaoReducaoSuplementacao> items)
        {
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();

            return _contaDer.PreparacaoMovimentacaoInterna(key, password, usuario?.Impressora132, programa, estrutura, items);
        }
    }
}
