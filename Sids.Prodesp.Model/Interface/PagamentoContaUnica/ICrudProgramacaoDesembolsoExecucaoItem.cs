using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudProgramacaoDesembolsoExecucaoItem : ICrudPagamentoContaUnica<PDExecucaoItem>
    {
        IEnumerable<PDExecucaoItem> FetchForGrid(PDExecucaoItem entity, int? tipoExecucao, DateTime? since, DateTime? until);
        int DeletarNaoAgrupados(int idExecucaoPD);
    }
}
