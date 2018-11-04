namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Base;
    using System.Collections.Generic;

    public interface ICrudEmpenho : ICrudBase<Model.Entity.Empenho.Empenho>
    {
        IEnumerable<Model.Entity.Empenho.Empenho> FetchForGrid(Model.Entity.Empenho.Empenho entity);
        Entity.Empenho.Empenho BuscarAssinaturas(Entity.Empenho.Empenho objModel);
    }
}
