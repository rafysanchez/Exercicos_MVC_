namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using System.Collections.Generic;

    public interface ICrudFuncionalidadeAcao : ICrudBase<FuncionalidadeAcao>
    {
        IEnumerable<FuncionalidadeAcao> FetchByFunctionality(Funcionalidade functionality);
        IEnumerable<FuncionalidadeAcao> FetchByAction(Acao action);
        IEnumerable<FuncionalidadeAcao> FetchByUserAndFunctionality(Usuario user, Funcionalidade functionality);
    }
}
