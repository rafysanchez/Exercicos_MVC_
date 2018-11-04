using Sids.Prodesp.Model.Entity.Seguranca;
using System.Collections.Generic;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface IAcao
    {
        string GetLogName();
        IEnumerable<Acao> Buscar(Acao objModel);
        IEnumerable<Acao> ObterAcaoPorFuncionalidade(Funcionalidade objModel);
        IEnumerable<Acao> ObterAcaoPorFuncionalidadeEUsuario(Usuario usuario, Funcionalidade recurso);
    }
}
