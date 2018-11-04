namespace Sids.Prodesp.Infrastructure.DataBase.Seguranca
{
    using Base;
    using Helpers;
    using Model.Entity.Seguranca;
    using Model.Interface.Seguranca;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class FuncionalidadeAcaoDal : BaseDal, ICrudFuncionalidadeAcao
    {
        public string GetTableName()
        {
            return "tb_recurso_acao";
        }

        public int Add(FuncionalidadeAcao entity)
        {
            return DataHelper.Get<int>("PR_RECURSO_ACAO_INCLUIR",
                new SqlParameter("@id_recurso", entity.Funcionalidade),
                new SqlParameter("@id_acao", entity.Acao)
            );
        }

        public int Edit(FuncionalidadeAcao entity)
        {
            return DataHelper.Get<int>("PR_RECURSOACAO_ALTERAR",
                new SqlParameter("@id_recurso_acao", entity.Codigo),
                new SqlParameter("@id_recurso", entity.Funcionalidade),
                new SqlParameter("@id_acao", entity.Acao),
                new SqlParameter("@bl_ativo", entity.Status)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RECURSO_ACAO_EXCLUIR",
                new SqlParameter("@id_recurso_acao", id)
            );
        }

        public IEnumerable<FuncionalidadeAcao> Fetch(FuncionalidadeAcao entity)
        {
            return DataHelper.List<FuncionalidadeAcao>("PR_RECURSO_ACAO_CONSULTAR",
                new SqlParameter("@id_recurso_acao", entity.Codigo),
                new SqlParameter("@id_recurso", entity.Funcionalidade),
                new SqlParameter("@id_acao", entity.Acao)
            );
        }

        public IEnumerable<FuncionalidadeAcao> GetPerfilAcaoByFuncionalidade(Funcionalidade functionality)
        {
            return DataHelper.List<FuncionalidadeAcao>("PR_GET_PERFILACAO_POR_PERFIL",
                new SqlParameter("@id_recurso", functionality.Codigo)
            );
        }

        public IEnumerable<FuncionalidadeAcao> FetchByAction(Acao action)
        {
            return Fetch(new FuncionalidadeAcao { Codigo = action.Id });
        }

        public IEnumerable<FuncionalidadeAcao> FetchByUserAndFunctionality(Usuario user, Funcionalidade functionality)
        {
            return DataHelper.List<FuncionalidadeAcao>("PR_GET_PERFILACAO_POR_USUARIO",
                new SqlParameter("@id_usuario", user.Codigo),
                new SqlParameter("@id_recurso", functionality.Codigo)
            );
        }

        public IEnumerable<FuncionalidadeAcao> FetchByFunctionality(Funcionalidade functionality)
        {
            return DataHelper.List<FuncionalidadeAcao>("PR_RECURSO_ACAO_CONSULTAR",
                new SqlParameter("@id_recurso_acao", null),
                new SqlParameter("@id_recurso", functionality.Codigo),
                new SqlParameter("@id_acao", null)
            );
        }
    }
}
