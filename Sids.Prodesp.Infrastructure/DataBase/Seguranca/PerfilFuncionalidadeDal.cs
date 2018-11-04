namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PerfilFuncionalidadeDal : BaseDal, ICrudPerfilFuncionalidade
    {
        public string GetTableName()
        {
            return "tb_perfil_recurso";
        }

        public int Add(PerfilFuncionalidade entity)
        {
            return DataHelper.Get<int>("PR_PERFILRECURSO_INCLUIR",
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_recurso", entity.Funcionalidade),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int Edit(PerfilFuncionalidade entity)
        {
            return DataHelper.Get<int>("PR_PERFILRECURSO_ALTERAR",
                new SqlParameter("@id_perfil_recurso", entity.Codigo),
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_recurso", entity.Funcionalidade),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PERFILRECURSO_EXCLUIR",
                new SqlParameter("@id_perfil_recurso", id)
            );
        }

        public IEnumerable<PerfilFuncionalidade> Fetch(PerfilFuncionalidade entity)
        {
            return DataHelper.List<PerfilFuncionalidade>("PR_PERFILRECURSO_CONSULTAR",
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_recurso", entity.Funcionalidade),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public IEnumerable<PerfilFuncionalidade> FetchByProfile(Perfil profile)
        {
            return DataHelper.List<PerfilFuncionalidade>("PR_GET_PERFILRECURSO_POR_PERFIL",
                new SqlParameter("@id_perfil", profile.Codigo)
            );
        }
        public IEnumerable<PerfilFuncionalidade> FetchByFunctionality(Funcionalidade functionality)
        {
            return Fetch(new PerfilFuncionalidade { Funcionalidade = functionality.Codigo });
        }

        public IEnumerable<PerfilFuncionalidade> FetchByUserAndFunctionalityId(Usuario user, int? functionalityId = null)
        {
            return DataHelper.List<PerfilFuncionalidade>("PR_GET_PERFILRECURSO_POR_USUARIO",
                new SqlParameter("@id_usuario", user.Codigo),
                new SqlParameter("@id_recurso", functionalityId)
            );
        }
    }
}
