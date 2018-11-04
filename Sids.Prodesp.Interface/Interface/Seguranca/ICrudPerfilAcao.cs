using Sids.Prodesp.Model.Entity.Seguranca;
using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ICrudPerfilAcao : ICrudBase<PerfilAcao>
    {
        IEnumerable<PerfilAcao> ObterPerfilAcaoPorPerfil(Perfil perfil);
        IEnumerable<PerfilAcao> ObterPerfilAcaoPorAcao(Acao logAcao);
        IEnumerable<PerfilAcao> ObterPerfilAcaoPorRecursoEUsuario(Usuario usuario, Funcionalidade recurso);
    }
}
