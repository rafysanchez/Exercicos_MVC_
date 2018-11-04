namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudModalidade
    {
        IEnumerable<Modalidade> Buscar(Modalidade objModel);
    }
}
