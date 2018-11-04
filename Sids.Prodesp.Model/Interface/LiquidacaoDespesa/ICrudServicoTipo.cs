namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Model.Entity.LiquidacaoDespesa;
    using System.Collections.Generic;

    public interface ICrudServicoTipo
    {
        IEnumerable<ServicoTipo> Fetch(ServicoTipo entity);
    }
}
