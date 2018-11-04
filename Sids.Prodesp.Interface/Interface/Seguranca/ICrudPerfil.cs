using Sids.Prodesp.Model.Entity.Seguranca;
using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ICrudPerfil : ICrudBase<Perfil>
    {
        IEnumerable<Perfil> ObterPerfilPorUsuario(Usuario objModel);
        int ObterNumeroUsuariosPorPerfil(int id);
    }
}
