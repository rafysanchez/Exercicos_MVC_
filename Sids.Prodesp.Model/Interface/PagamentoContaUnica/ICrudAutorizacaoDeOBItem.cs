using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudAutorizacaoDeOBItem : ICrudPagamentoContaUnica<OBAutorizacaoItem>
    {
        IEnumerable<OBAutorizacaoItem> FetchForGrid(OBAutorizacaoItem entity, DateTime? since, DateTime? until);

        IEnumerable<OBAutorizacaoItem> Fetch(int id);

        IEnumerable<OBAutorizacaoItem> FetchForGrid(OBAutorizacaoItem entity, int? tipoExecucao, DateTime? since, DateTime? until);

        OBAutorizacaoItem GetDadosApoio(int tipo, string dsNumPD);

        int DeletarNaoAgrupados(int idAutorizacaoOB);

        OBAutorizacaoItem Get(int id, string numOb);
    }
}
