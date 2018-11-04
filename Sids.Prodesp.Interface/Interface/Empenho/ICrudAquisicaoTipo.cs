namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudAquisicaoTipo
    {
        IEnumerable<AquisicaoTipo> Buscar(AquisicaoTipo objModel);
    }
}
