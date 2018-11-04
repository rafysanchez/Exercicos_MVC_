namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class MenuItemDal : BaseDal, ICrudMenuItem
    {
        public string GetTableName()
        {
            return "tb_menu_item";
        }

        public int Add(MenuItem entity)
        {
            return DataHelper.Get<int>("PR_MENUITEM_INCLUIR",
                new SqlParameter("@id_menu", entity.Menu),
                new SqlParameter("@id_recurso", entity.Recurso),
                new SqlParameter("@ds_rotulo", entity.Rotulo),
                new SqlParameter("@ds_abrir_em", entity.AbrirEm),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@bl_ativo", entity.Status),
                new SqlParameter("@dt_criacao", entity.DataCriacao),
                new SqlParameter("@id_usuario_criacao", entity.UsuarioCriacao)
            );
        }

        public int Edit(MenuItem entity)
        {
            return DataHelper.Get<int>("PR_MENUITEM_ALTERAR",
                new SqlParameter("@id_menu_item", entity.Codigo),
                new SqlParameter("@id_menu", entity.Menu),
                new SqlParameter("@id_recurso", entity.Recurso),
                new SqlParameter("@ds_rotulo", entity.Rotulo),
                new SqlParameter("@ds_abrir_em", entity.AbrirEm),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_MENUITEM_EXCLUIR", 
                new SqlParameter("@id_menu_item", id)
            );
        }

        public int Delete(MenuItem entity)
        {
            return DataHelper.Get<int>("PR_MENUITEM_EXCLUIR", 
                new SqlParameter("@id_menu_item", entity.Codigo)
            );
        }

        public IEnumerable<MenuItem> Fetch(MenuItem entity)
        {
            return DataHelper.List<MenuItem>("PR_MENUITEM_CONSULTAR",
                new SqlParameter("@id_menu_item", entity.Codigo),
                new SqlParameter("@id_menu", entity.Menu),
                new SqlParameter("@id_recurso", entity.Recurso),
                new SqlParameter("@ds_rotulo", entity.Rotulo ?? string.Empty),
                new SqlParameter("@ds_abrir_em", Convert.ToString(entity.AbrirEm)),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public IEnumerable<MenuItem> FetchByItem(MenuItem entity)
        {
            return DataHelper.List<MenuItem>("SPO_MENUITEM_HASSUBITENS",
                new SqlParameter("@id_menuitempai", entity.Codigo)
            );
        }

        public IEnumerable<MenuItem> FetchByMenu(Menu menu)
        {
            return DataHelper.List<MenuItem>("PR_GET_MENU_ITEM_POR_MENU",
                new SqlParameter("@id_menu", menu.Codigo)
            );
        }

        public IEnumerable<MenuItem> FetchByUser(Usuario user)
        {
            return DataHelper.List<MenuItem>("PR_GET_MENU_ITEM_POR_USUARIO",
                new SqlParameter("@id_usuario", user.Codigo)
            );
        }

        public IEnumerable<Url> FetchUrlById(int urlId)
        {
            return DataHelper.List<Url>("PR_MENU_URL_CONSULTAR",
                new SqlParameter("@id_menu_url", urlId)
            );
        }
    }
}
