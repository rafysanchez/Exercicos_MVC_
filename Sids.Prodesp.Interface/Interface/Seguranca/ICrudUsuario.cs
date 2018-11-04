using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Interface.Base;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ICrudUsuario : ICrudBase<Usuario>
    {
        int AtualizarUsuarioAD(Usuario objModel);
        int AtualizarDataUltumoAcesso(Usuario objModel);
        int AtualizarDataExpirarSenha(Usuario objModel);
    }
}
