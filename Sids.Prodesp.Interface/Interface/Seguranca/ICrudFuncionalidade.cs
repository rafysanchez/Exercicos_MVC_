using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    using Model.Entity.Seguranca;
    public interface ICrudFuncionalidade: ICrudBase<Funcionalidade>
    {
        IEnumerable<Funcionalidade> ObterFuncionalidadePorUsuario(Usuario objModel);
        IEnumerable<Funcionalidade> ObterFuncionalidadePorId(List<int> ids);
    }
}
