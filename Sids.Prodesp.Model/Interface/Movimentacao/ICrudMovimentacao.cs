using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Interface.PagamentoContaDer.Base;
using Sids.Prodesp.Model.Interface.Movimentacao.Base;
using Sids.Prodesp.Model.Entity.Movimentacao;
using System.Collections.Generic;
using Sids.Prodesp.Model.ValueObject;

namespace Sids.Prodesp.Model.Interface.PagamentoContaDer
{
    public interface ICrudMovimentacao : ICrudMovimentacao<MovimentacaoOrcamentaria>
    {
        int GetLastGroup();

        IEnumerable<MovimentacaoCancelamento> Fetch(MovimentacaoCancelamento obj);
        
        IEnumerable<MovimentacaoReducaoSuplementacao> Fetch(MovimentacaoReducaoSuplementacao obj);

        IEnumerable<MovimentacaoDistribuicao> Fetch(MovimentacaoDistribuicao obj);

        IEnumerable<MovimentacaoNotaDeCredito> Fetch(MovimentacaoNotaDeCredito obj);
        int Save(MovimentacaoCancelamento item);
        int Save(MovimentacaoNotaDeCredito item);
        int Save(MovimentacaoDistribuicao item);
        int Save(MovimentacaoReducaoSuplementacao item);
        int Remove(MovimentacaoCancelamento objModel);
        int Remove(MovimentacaoReducaoSuplementacao objModel);
        int Remove(MovimentacaoDistribuicao objModel);
        int Remove(MovimentacaoNotaDeCredito objModel);
        AssinaturasVo BuscarUltimaAssinatura();
    }
}
