namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudAquisicaoTipo
    {
        IEnumerable<AquisicaoTipo> Fetch(AquisicaoTipo entity);
    }
}
