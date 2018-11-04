using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;
using Sids.Prodesp.Model.Entity.Configuracao;

namespace Sids.Prodesp.Interface.Interface.Configuracao
{
    public interface ICrudPrograma: ICrudBase<Programa>
    {
        IEnumerable<int> GetAnosPrograma();
        IEnumerable<int> GerarEstruturaAnoAtual(int ano);
    }
}
