namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface IAcao
    {
        string GetTableName();
        IEnumerable<Acao> Fetch(Acao entity);
        IEnumerable<Acao> FetchByFunctionality(Funcionalidade functionality);
        IEnumerable<Acao> FetchByUserAndFunctionality(Usuario user, Funcionalidade functionality);
    }
}
