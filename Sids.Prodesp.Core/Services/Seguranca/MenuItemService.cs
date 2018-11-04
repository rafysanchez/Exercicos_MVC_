namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Enum;
    using System.Web;

    public class MenuItemService : BaseService
    {
        const string LogTable = "TB_MENU_ITEM";
        ICrudMenuItem menuItem;

        public MenuItemService(ILogError l, ICrudMenuItem m)
            : base(l)
        {
            menuItem = m;
        }

        public AcaoEfetuada Salvar(MenuItem obj, int recursoId, short actionId)
        {
            try
            {
                if (obj.Codigo == 0)
                {
                    PreInsertModel(obj);
                    int id = menuItem.Add(obj);
                }
                else
                {
                    menuItem.Edit(obj);
                }
                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        public AcaoEfetuada AlterarStatus(MenuItem obj, int recursoId, short actionId)
        {
            try
            {
                obj.Status = obj.Status == true ? false : true;
                menuItem.Edit(obj);
                return LogSucesso(actionId, recursoId, obj.Codigo.ToString());
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        public void Excluir(int id)
        {
            try
            {
                menuItem.Remove(id);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<MenuItem> Buscar(MenuItem obj)
        {
            try
            {
                return menuItem.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<MenuItem> GetSubmenuItems(MenuItem obj)
        {
            try
            {
                return menuItem.FetchByItem(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<MenuItem> GetMenuItemByMenu(Menu obj)
        {
            try
            {
                return menuItem.FetchByMenu(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<MenuItem> GetMenuItemByUsuario(Usuario objModel)
        {
            try
            {

                if (objModel != null)
                {
                    List<MenuItem> itens = menuItem.FetchByUser(objModel).ToList();

                    HttpContext.Current.Session["Menu"] = itens;
                }
                else
                {
                    HttpContext.Current.Session["Menu"] = new List<MenuItem>();
                }
                return (List<MenuItem>)HttpContext.Current.Session["Menu"];
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<Url> GetMenuUrl(int id)
        {
            try
            {
                return menuItem.FetchUrlById(id).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

    }
}
