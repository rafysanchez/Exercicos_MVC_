using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ISistema
    {
        string GetLogName();
        IEnumerable<Sistema> Buscar(Sistema objModel);
    }
}
