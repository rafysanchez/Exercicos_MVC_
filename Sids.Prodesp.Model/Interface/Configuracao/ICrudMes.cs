namespace Sids.Prodesp.Model.Interface.Configuracao
{
    using Model.Entity.Configuracao;
    using System.Collections.Generic;

    public interface ICrudMes
    {
        Mes Get(int id);
        IEnumerable<Mes> FetchAll();
    }
}