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
    using Model.Exceptions;

    public class MenuService : BaseService
    {
        ICrudMenu menu;
        ICrudMenuItem menuItem;

        public MenuService(ILogError l, ICrudMenu m, ICrudMenuItem i)
            : base(l)
        {
            menu = m;
            menuItem = i;
        }

        public AcaoEfetuada Salvar(Menu obj,int recursoId,short actionId)
        {
            try
            {
                Menu m = menu.Fetch(new Menu { Descricao = obj.Descricao }).FirstOrDefault();
                if (m != null && m.Codigo != obj.Codigo)
                {
                    throw new SidsException(MensagemGeral.MGDuplicidade);
                }
                if (obj.Codigo == 0)
                {
                    PreInsertModel(obj);
                    int id = menu.Add(obj);
                    return LogSucesso(actionId, recursoId, id.ToString());

                }
                else
                {
                    if (obj.Status == false &&
                        menuItem.Fetch(new MenuItem { Menu = obj.Codigo, Status = true }).Any())
                    {
                        throw new SidsException("Esse Menu não pode ser inativado pois está vinculado a um Item de Menu ativo, favor verificar!");
                    }
                    else
                    {
                        menu.Edit(obj);
                        return LogSucesso(actionId, recursoId, obj.Codigo.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex,actionId,recursoId);
            }
        }
        public void Excluir(int id,short actionId, int recursoId)
        {
            try
            {
                menu.Remove(id);
                LogSucesso(actionId, recursoId, id.ToString());
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }
        public List<Menu> Buscar(Menu obj)
        {
            try
            {
                return menu.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

    }

}
