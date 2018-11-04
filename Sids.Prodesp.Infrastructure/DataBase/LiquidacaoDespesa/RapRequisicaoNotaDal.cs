namespace Sids.Prodesp.Infrastructure.DataBase.Empenho
{
    using Helpers;
    using Model.Base.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class RapRequisicaoNotaDal : ICrudRapRequisicaoNota
    {
        public int Edit(LiquidacaoDespesaNota entity)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_NOTA_ALTERAR",
                new SqlParameter("@id_rap_requisicao_nota", entity.Id),
                new SqlParameter("@tb_rap_requisicao_id_rap_requisicao", entity.SubempenhoId),
                new SqlParameter("@cd_nota", entity.CodigoNotaFiscal)
            );
        }

        public IEnumerable<LiquidacaoDespesaNota> Fetch(LiquidacaoDespesaNota entity)
        {
            return DataHelper.List<LiquidacaoDespesaNota>("PR_RAP_REQUISICAO_NOTA_CONSULTAR",
                new SqlParameter("@id_rap_requisicao_nota", entity.Id),
                new SqlParameter("@tb_rap_requisicao_id_rap_requisicao", entity.SubempenhoId)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_NOTA_EXCLUIR",
                new SqlParameter("@id_rap_requisicao_nota", id)
            );
        }

        public int Add(LiquidacaoDespesaNota entity)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_NOTA_INCLUIR",
                new SqlParameter("@tb_rap_requisicao_id_rap_requisicao", entity.SubempenhoId),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@cd_nota", entity.CodigoNotaFiscal)
            );
        }

        public string GetTableName()
        {
            return "tb_rap_requisicao_nota";
        }

        public int Save(LiquidacaoDespesaNota entity)
        {
            return DataHelper.Get<int>("PR_RAP_REQUISICAO_NOTA_SALVAR",
                new SqlParameter("@id_rap_requisicao_nota", entity.Id),
                new SqlParameter("@tb_rap_requisicao_id_rap_requisicao", entity.SubempenhoId),
                new SqlParameter("@nr_ordem", entity.Ordem),
                new SqlParameter("@cd_nota", entity.CodigoNotaFiscal)
            );
        }
    }
}
