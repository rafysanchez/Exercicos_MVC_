namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Entity.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;

    public interface ICrudRapRequisicao : ICrudBase<RapRequisicao>
    {
        IEnumerable<RapRequisicao> FetchForGrid(RapRequisicao entity, DateTime since, DateTime until);
        RapRequisicao GetLastSignatures(RapRequisicao entity);
        int Save(RapRequisicao entity);
        RapRequisicao Get(int id);
    }
}
