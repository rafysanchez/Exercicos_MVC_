using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;
using Sids.Prodesp.Model.Entity.Configuracao;

namespace Sids.Prodesp.Interface.Interface.Configuracao
{
    public interface ICrudEstrutura: ICrudBase<Estrutura>
    {
        IEnumerable<Estrutura> ObterPorPrograma(Programa objModel);
    }
}
