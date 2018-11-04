namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ICrudPerfilFuncionalidade : ICrudBase<PerfilFuncionalidade>
    {
        IEnumerable<PerfilFuncionalidade> FetchByProfile(Perfil profile);
        IEnumerable<PerfilFuncionalidade> FetchByFunctionality(Funcionalidade functionality);
        IEnumerable<PerfilFuncionalidade> FetchByUserAndFunctionalityId(Usuario user, int? functionalityId = null);
    }
}
