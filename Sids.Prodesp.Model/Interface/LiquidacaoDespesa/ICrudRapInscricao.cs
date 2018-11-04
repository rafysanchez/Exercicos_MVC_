namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Entity.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;

    public interface ICrudRapInscricao : ICrudBase<RapInscricao>
    {
        IEnumerable<RapInscricao> FetchForGrid(RapInscricao entity, DateTime since, DateTime until);
        RapInscricao GetLastSignatures(RapInscricao entity);
        int Save(RapInscricao entity);
        RapInscricao Get(int id);
    }
}
