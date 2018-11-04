using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudProgramacaoDesembolsoAgrupamento : ICrudLists<ProgramacaoDesembolsoAgrupamento>
    {
        int GetLastGroup();


        IEnumerable<ProgramacaoDesembolsoAgrupamento> FetchBloqueio(ProgramacaoDesembolsoAgrupamento entity);




    }
}
