namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudOrigemMaterial
    {
        IEnumerable<OrigemMaterial> Buscar(OrigemMaterial objModel);
    }
}
