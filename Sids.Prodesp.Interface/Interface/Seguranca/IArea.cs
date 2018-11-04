using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface IArea
    {
        string GetLogName();
        IEnumerable<Area> Buscar(Area objModel);
    }
}
