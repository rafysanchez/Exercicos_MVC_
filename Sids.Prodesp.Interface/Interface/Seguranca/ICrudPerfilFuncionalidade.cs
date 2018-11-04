using Sids.Prodesp.Model.Entity.Seguranca;
using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ICrudPerfilFuncionalidade : ICrudBase<PerfilFuncionalidade>
    {
        IEnumerable<PerfilFuncionalidade> ObterPerfilRecursoPorPerfil(Perfil objModel);
        IEnumerable<PerfilFuncionalidade> ObterPerfilRecursoPorRecurso(Funcionalidade objModel);
        IEnumerable<PerfilFuncionalidade> ObterPerfilRecursoPorUsuario(Usuario objModel, int? idRecurso = null);
    }
}
