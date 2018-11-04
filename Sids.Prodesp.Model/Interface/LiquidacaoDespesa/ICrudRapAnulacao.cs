namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Entity.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;

    public interface ICrudRapAnulacao : ICrudBase<RapAnulacao>
    {
        IEnumerable<RapAnulacao> FetchForGrid(RapAnulacao entity, DateTime since, DateTime until);
        RapAnulacao GetLastSignatures(RapAnulacao entity);
        int Save(RapAnulacao entity);
        RapAnulacao Get(int id);
    }
}
