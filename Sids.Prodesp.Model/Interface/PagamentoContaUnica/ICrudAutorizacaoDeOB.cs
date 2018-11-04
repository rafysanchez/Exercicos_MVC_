using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System.Collections.Generic;
using System;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudAutorizacaoDeOB : ICrudPagamentoContaUnica<OBAutorizacao>
    {
        IEnumerable<PDExecucaoTipoPagamento> FetchTiposPagamento();
        IEnumerable<OBAutorizacao> FetchForGrid(OBAutorizacao entity, DateTime? since, DateTime? until);
        OBAutorizacao Get(int id, string numOb);
    }
}
