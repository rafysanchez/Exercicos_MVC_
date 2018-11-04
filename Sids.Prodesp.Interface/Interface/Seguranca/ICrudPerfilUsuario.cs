using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Interface.Base;
using System.Collections.Generic;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ICrudPerfilUsuario : ICrudBase<PerfilUsuario>
    {
        new int Excluir(int id);
        IEnumerable<PerfilUsuario> ObterPerfilUsuarioPorPerfil(Perfil objModel);
        IEnumerable<PerfilUsuario> ObterPerfilUsuarioPorUsuario(Usuario objModel);
    }
}
