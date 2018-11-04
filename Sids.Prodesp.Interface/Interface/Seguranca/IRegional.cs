using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface IRegional
    {
        string GetLogName();
        IEnumerable<Regional> Buscar(Regional objModel);
    }
}
