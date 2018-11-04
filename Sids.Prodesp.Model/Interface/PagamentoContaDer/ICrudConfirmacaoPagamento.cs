using Sids.Prodesp.Model.Interface.PagamentoContaDer.Base;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;


namespace Sids.Prodesp.Model.Interface.PagamentoContaDer
{
    public interface ICrudConfirmacaoPagamento : ICrudPagamentoContaDer<ConfirmacaoPagamento>
    {   
        int Add(ConfirmacaoPagamento entity);
        int Add(PDExecucao entity);
        int Add(OBAutorizacao entity);
        int Edit(ConfirmacaoPagamento entity);
        int SavePayment(ConfirmacaoPagamento entity, string tipo);
    }
}
