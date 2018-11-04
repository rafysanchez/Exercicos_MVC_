namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoTipo
    {
        IEnumerable<EmpenhoTipo> Buscar(EmpenhoTipo objModel);
    }
}
