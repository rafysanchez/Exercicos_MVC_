namespace Sids.Prodesp.Infrastructure.DataBase.Configuracao
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Interface.Configuracao;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ProgramaDal : ICrudPrograma
    {
        public int Remove(int id)             
        {
            return DataHelper.Get<int>("PR_PROGRAMA_EXCLUIR",
                new SqlParameter("@id_programa", id));
        }

        public IEnumerable<int> CopyProgramStructureFromYear(int year)
        {
            return DataHelper.List<int>("PR_GERAR_PROGRAMA",
                new SqlParameter("@nr_ano_referencia", year));
        }

        public IEnumerable<int> FetchProgramYears()
        {
            return DataHelper.List<int>("PR_GET_ANOS_PROGRAMA");
        }

        public string GetTableName()
        {
            return "tb_programa";
        }

        public int Add(Programa entity)
        {
            return DataHelper.Get<int>("PR_PROGRAMA_INCLUIR",
                new SqlParameter("@cd_ptres", entity.Ptres),
                new SqlParameter("@cd_cfp", entity.Cfp),
                new SqlParameter("@ds_programa", entity.Descricao),
                new SqlParameter("@nr_ano_referencia", entity.Ano)
            );
        }

        public IEnumerable<Programa> Fetch(Programa entity)
        {
            return DataHelper.List<Programa>("PR_PROGRAMA_CONSULTAR",
                new SqlParameter("@id_programa", entity.Codigo),
                new SqlParameter("@cd_ptres", entity.Ptres),
                new SqlParameter("@cd_cfp", entity.Cfp),
                new SqlParameter("@ds_programa", entity.Descricao),
                new SqlParameter("@nr_ano_referencia", entity.Ano)
            );
        }

        public int Edit(Programa entity)
        {
            return DataHelper.Get<int>("PR_PROGRAMA_ALTERAR",
                new SqlParameter("@id_programa", entity.Codigo),
                new SqlParameter("@cd_ptres", entity.Ptres),
                new SqlParameter("@cd_cfp", entity.Cfp),
                new SqlParameter("@ds_programa", entity.Descricao),
                new SqlParameter("@nr_ano_referencia", entity.Ano)
            );
        }
    }
}
