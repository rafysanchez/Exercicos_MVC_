namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ICrudPerfilAcao : ICrudBase<PerfilAcao>
    {
        IEnumerable<PerfilAcao> FetchByProfile(Perfil profile);
        IEnumerable<PerfilAcao> FetchByAction(Acao action);
        IEnumerable<PerfilAcao> FetchByUserAndFunctionality(Usuario user, Funcionalidade functionality);
    }
}
