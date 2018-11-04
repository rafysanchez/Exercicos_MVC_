namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class MenuDal : BaseDal, ICrudMenu
    {
        public int Add(Menu entity)
        {
            return DataHelper.Get<int>("PR_MENU_INCLUIR",
                new SqlParameter("@id_recurso", entity.Recurso),
                new SqlParameter("@ds_menu", entity.Descricao),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@bl_ativo", entity.Status),
                new SqlParameter("@dt_criacao", entity.DataCriacao)
            );
        }

        public int Edit(Menu entity)
        {
            return DataHelper.Get<int>("PR_MENU_ALTERAR",
                new SqlParameter("@id_menu", entity.Codigo),
                new SqlParameter("@id_recurso", entity.Recurso),
                new SqlParameter("@ds_menu", entity.Descricao),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_MENU_EXCLUIR", 
                new SqlParameter("@id_menu", id)
            );
        }

        public IEnumerable<Menu> Fetch(Menu entity)
        {
            return DataHelper.List<Menu>("PR_MENU_CONSULTAR",
                new SqlParameter("@id_menu", entity.Codigo),
                new SqlParameter("@id_recurso", entity.Recurso),
                new SqlParameter("@ds_menu", entity.Descricao),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public string GetTableName()
        {
            return "tb_menu";
        }
    }
}
