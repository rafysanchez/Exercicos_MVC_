namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class FuncionalidadeDal : BaseDal, ICrudFuncionalidade
    {
        public string GetTableName()
        {
            return "tb_recurso";
        }

        public int Add(Funcionalidade entity)
        {
            return DataHelper.Get<int>("PR_RECURSO_INCLUIR",
                new SqlParameter("@no_recurso", entity.Nome),
                new SqlParameter("@ds_recurso", entity.Descricao),
                new SqlParameter("@ds_keywords", entity.Keywords),
                new SqlParameter("@ds_url", entity.URL),
                new SqlParameter("@bl_publico", entity.Publico),
                new SqlParameter("@bl_ativo", entity.Status),
                new SqlParameter("@dt_criacao", entity.DataCriacao),
                new SqlParameter("@id_menu_url", entity.MenuUrlId)
            );
        }

        public int Edit(Funcionalidade entity)
        {
            return DataHelper.Get<int>("PR_RECURSO_ALTERAR",
                new SqlParameter("@id_recurso", entity.Codigo),
                new SqlParameter("@no_recurso", entity.Nome),
                new SqlParameter("@ds_recurso", entity.Descricao),
                new SqlParameter("@ds_keywords", entity.Keywords),
                new SqlParameter("@ds_url", entity.URL),
                new SqlParameter("@bl_publico", entity.Publico),
                new SqlParameter("@bl_ativo", entity.Status),
                new SqlParameter("@id_menu_url", entity.MenuUrlId)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RECURSO_EXCLUIR", 
                new SqlParameter("@id_recurso", id)
            );
        }

        public IEnumerable<Funcionalidade> Fetch(Funcionalidade entity)
        {
            return DataHelper.List<Funcionalidade>("PR_RECURSO_CONSULTAR",
                new SqlParameter("@id_recurso", entity.Codigo),
                new SqlParameter("@no_recurso", entity.Nome),
                new SqlParameter("@ds_recurso", entity.Descricao),
                new SqlParameter("@ds_keywords", entity.Keywords),
                new SqlParameter("@ds_url", entity.URL),
                new SqlParameter("@bl_publico", entity.Publico),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public IEnumerable<Funcionalidade> FetchByUser(Usuario user)
        {
            return DataHelper.List<Funcionalidade>("PR_GET_RECURSOS_POR_USUARIO", 
                new SqlParameter("@id_usuario", user.Codigo)
            );
        }

        public IEnumerable<Funcionalidade> FetchByIds(IEnumerable<int> ids)
        {
            return DataHelper.List<Funcionalidade>("PR_GET_RECURSOS_POR_ID", 
                new SqlParameter("@ids", string.Join(",", ids))
            );
        }
    }
}
