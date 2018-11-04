namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Base;
    using System.Collections.Generic;

    public interface ICrudEmpenho : ICrudBase<Model.Entity.Empenho.Empenho>
    {
        IEnumerable<Model.Entity.Empenho.Empenho> BuscarGrid(Model.Entity.Empenho.Empenho objModel);
        Model.Entity.Empenho.Empenho BuscarAssinaturas(Model.Entity.Empenho.Empenho objModel);
    }
}
