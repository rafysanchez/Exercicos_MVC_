namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PerfilUsuarioDal : BaseDal, ICrudPerfilUsuario
    {
        public string GetTableName()
        {
            return "tb_perfil_usuario";
        }

        public int Add(PerfilUsuario entity)
        {
            return DataHelper.Get<int>("PR_PERFILUSUARIO_INCLUIR",
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_usuario", entity.Usuario),
                new SqlParameter("@bl_ativo", entity.Status),
                new SqlParameter("@dt_criacao", entity.DataCriacao)
            );
        }

        public int Edit(PerfilUsuario entity)
        {
            return DataHelper.Get<int>("SPO_PERFILUSUARIO_ALTERAR",
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_usuario", entity.Usuario),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int Delete(PerfilUsuario entity)
        {
            return DataHelper.Get<int>("PR_PERFILUSUARIO_EXCLUIR",
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_usuario", entity.Usuario),
                new SqlParameter("@id_perfil_usuario", entity.Codigo)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PERFILUSUARIO_EXCLUIR",
                new SqlParameter("@id_perfil_usuario", id)
            );
        }


        public IEnumerable<PerfilUsuario> Fetch(PerfilUsuario entity)
        {
            return DataHelper.List<PerfilUsuario>("PR_PERFILUSUARIO_CONSULTAR",
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_usuario", entity.Usuario),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public IEnumerable<PerfilUsuario> FetchByProfile(Perfil entity)
        {
            return Fetch(new PerfilUsuario { Perfil = entity.Codigo });
        }

        public IEnumerable<PerfilUsuario> FetchByUser(Usuario entity)
        {
            return DataHelper.List<PerfilUsuario>("PR_GET_PERFILUSUARIO_POR_USUARIO",
                new SqlParameter("@id_usuario", (entity.Codigo == 0 ? default(string) : entity.Codigo.ToString()))
            );
        }
    }
}
