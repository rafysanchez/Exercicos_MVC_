namespace Sids.Prodesp.Infrastructure.DataBase.Configuracao
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Interface.Configuracao;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class EstruturaDal : ICrudEstrutura
    {
        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_ESTRUTURA_EXCLUIR",
                new SqlParameter("@id_estrutura", id)
            );
        }

        public string GetTableName()
        {
            return "tb_estrutura";
        }

        public int Add(Estrutura entity)
        {
            return DataHelper.Get<int>("PR_ESTRUTURA_INCLUIR",
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@ds_nomenclatura", entity.Nomenclatura),
                new SqlParameter("@cd_natureza", entity.Natureza),
                new SqlParameter("@cd_macro", entity.Macro),
                new SqlParameter("@cd_codigo_aplicacao", entity.Aplicacao),
                new SqlParameter("@cd_fonte", entity.Fonte)
            );
        }

        public IEnumerable<Estrutura> Fetch(Estrutura entity)
        {
            var paramCodigo = new SqlParameter("@id_estrutura", entity.Codigo);
            var paramPrograma = new SqlParameter("@id_programa", entity.Programa);
            var paramNomenclatura = new SqlParameter("@ds_nomenclatura", entity.Nomenclatura);
            var paramNatureza = new SqlParameter("@cd_natureza", entity.Natureza);
            var paramMacro = new SqlParameter("@cd_macro", entity.Macro);
            var paramAplicacao = new SqlParameter("@cd_codigo_aplicacao", entity.Aplicacao);
            var paramFonte = new SqlParameter("@cd_fonte", entity.Fonte);

            var dbResult = DataHelper.List<Estrutura>("PR_ESTRUTURA_CONSULTAR", paramCodigo, paramPrograma, paramNomenclatura, paramNatureza, paramMacro, paramAplicacao, paramFonte);

            return dbResult;
        }

        public IEnumerable<Estrutura> FetchByProgram(Programa entity)
        {
            return DataHelper.List<Estrutura>("PR_GET_ESTRUTURA_POR_PROGRAMA",
                new SqlParameter("@id_programa", entity.Codigo),
                new SqlParameter("@cd_ptres", entity.Ptres),
                new SqlParameter("@cd_cfp", entity.Cfp),
                new SqlParameter("@ds_programa", entity.Descricao),
                new SqlParameter("@nr_ano_referencia", entity.Ano)
            );
        }

        public int Edit(Estrutura entity)
        {
            return DataHelper.Get<int>("PR_ESTRUTURA_ALTERAR",
                new SqlParameter("@id_estrutura", entity.Codigo),
                new SqlParameter("@id_programa", entity.Programa),
                new SqlParameter("@ds_nomenclatura", entity.Nomenclatura),
                new SqlParameter("@cd_natureza", entity.Natureza),
                new SqlParameter("@cd_macro", entity.Macro),
                new SqlParameter("@cd_codigo_aplicacao", entity.Aplicacao),
                new SqlParameter("@cd_fonte", entity.Fonte)
            );
        }

    }
}
