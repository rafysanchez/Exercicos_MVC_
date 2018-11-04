using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;
using System;
using System.Collections.Generic;

namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica
{
    public interface ICrudImpressaoRelacaoRERT : ICrudPagamentoContaUnica<ImpressaoRelacaoRERT>
    {
        IEnumerable<ImpressaoRelacaoReRtConsultaVo> Fetch(ImpressaoRelacaoReRtConsultaVo entity);

        IEnumerable<ImpressaoRelacaoReRtConsultaVo> Fetch(int id);
    }
}
