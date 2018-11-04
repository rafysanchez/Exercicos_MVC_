namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PerfilAcaoDal : BaseDal, ICrudPerfilAcao
    {
        public string GetTableName()
        {
            return "tb_perfil_acao";
        }

        public int Add(PerfilAcao entity)
        {
            return DataHelper.Get<int>("PR_PERFIL_ACAO_INCLUIR",
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_acao", entity.Acao),
                new SqlParameter("@id_recurso_acao", entity.Funcionalidade)
            );
        }

        public int Edit(PerfilAcao entity)
        {
            return DataHelper.Get<int>("PR_PERFILACAO_ALTERAR",
                new SqlParameter("@id_perfil_recurso", entity.Codigo),
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_acao", entity.Acao),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_PERFIL_ACAO_EXCLUIR",
                new SqlParameter("@id_perfil_acao", id)
            );
        }

        public IEnumerable<PerfilAcao> Fetch(PerfilAcao entity)
        {
            return DataHelper.List<PerfilAcao>("PR_PERFIL_ACAO_CONSULTAR",
                new SqlParameter("@id_perfil_acao", entity.Codigo),
                new SqlParameter("@id_acao", entity.Acao),
                new SqlParameter("@id_perfil", entity.Perfil),
                new SqlParameter("@id_recurso", entity.Funcionalidade)
            );
        }

        public IEnumerable<PerfilAcao> FetchByProfile(Perfil profile)
        {
            return DataHelper.List<PerfilAcao>("PR_GET_PERFILACAO_POR_PERFIL",
                new SqlParameter("@id_perfil", profile.Codigo)
            );
        }
        public IEnumerable<PerfilAcao> FetchByAction(Acao action)
        {
            return Fetch(new PerfilAcao { Codigo = action.Id });
        }

        public IEnumerable<PerfilAcao> FetchByUserAndFunctionality(Usuario user, Funcionalidade functionality)
        {
            return DataHelper.List<PerfilAcao>("PR_GET_PERFILACAO_POR_USUARIO",
                new SqlParameter("@id_usuario", user.Codigo),
                new SqlParameter("@id_recurso", functionality.Codigo)
            );
        }
    }
}
