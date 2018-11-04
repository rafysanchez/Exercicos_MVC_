using Sids.Prodesp.Model.Entity.Seguranca;
using System.Collections.Generic;
using Sids.Prodesp.Interface.Base;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface ICrudMenuItem : ICrudBase<MenuItem>
    {
        IEnumerable<MenuItem> GetSubMenuItems(MenuItem objModel);
        IEnumerable<MenuItem> GetMenuItemByMenu(Menu objModel);
        IEnumerable<MenuItem> GetMenuItemByUsuario(Usuario objModel);
    }
}
