using Sids.Prodesp.Model.Entity.Seguranca;
using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ICrudFuncionalidadeAcao : ICrudBase<FuncionalidadeAcao>
    {
        IEnumerable<FuncionalidadeAcao> ObterFuncionalidadeAcaoPorFuncionalidade(Funcionalidade funcionalidade);
        IEnumerable<FuncionalidadeAcao> ObterFuncionalidadeAcaoPorAcao(Acao logAcao);
        IEnumerable<FuncionalidadeAcao> ObterFuncionalidadeAcaoPorFuncionalidadeEUsuario(Usuario usuario, Funcionalidade recurso);
    }
}
