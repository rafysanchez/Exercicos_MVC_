using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudProgramacaoDesembolsoExecucao : ICrudPagamentoContaUnica<PDExecucao>
    {
        IEnumerable<PDExecucaoTipoExecucao> FetchTiposExecucao();
        IEnumerable<PDExecucaoTipoPagamento> FetchTiposPagamento();
        IEnumerable<ProgramacaoDesembolso> Fetch(ProgramacaoDesembolso entity);
        ProgramacaoDesembolso Get(string dsNumPD);
        ProgramacaoDesembolsoDadosApoio GetDadosApoio(int tipo, string dsNumPD);
        IEnumerable<ProgramacaoDesembolso> ListDesdobradas(string dsNumPD);
    }
}
