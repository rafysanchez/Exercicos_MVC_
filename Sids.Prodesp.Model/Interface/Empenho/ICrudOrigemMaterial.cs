namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudOrigemMaterial
    {
        IEnumerable<OrigemMaterial> Fetch(OrigemMaterial entity);
    }
}
