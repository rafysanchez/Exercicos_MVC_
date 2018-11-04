namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Base.LiquidacaoDespesa;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SubempenhoCancelamentoEventoDal : ICrudSubempenhoCancelamentoEvento
    {
        public int Edit(LiquidacaoDespesaEvento entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_EVENTO_ALTERAR",
                new SqlParameter("@id_subempenho_cancelamento_evento", entity.Id),
                new SqlParameter("@tb_subempenho_cancelamento_id_subempenho_cancelamento", entity.SubempenhoId),
                new SqlParameter("@cd_fonte", entity.Fonte),
                new SqlParameter("@cd_evento", entity.NumeroEvento),
                new SqlParameter("@cd_classificacao", entity.Classificacao),
                new SqlParameter("@ds_inscricao", entity.InscricaoEvento),
                new SqlParameter("@vl_evento", entity.ValorUnitario)
            );
        }

        public IEnumerable<LiquidacaoDespesaEvento> Fetch(LiquidacaoDespesaEvento entity)
        {
            return DataHelper.List<LiquidacaoDespesaEvento>("PR_SUBEMPENHO_CANCELAMENTO_EVENTO_CONSULTAR",
                new SqlParameter("@id_subempenho_cancelamento_evento", entity.Id),
                new SqlParameter("@tb_subempenho_cancelamento_id_subempenho_cancelamento", entity.SubempenhoId)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_EVENTO_EXCLUIR",
                new SqlParameter("@id_subempenho_cancelamento_evento", id)
            );
        }

        public int Add(LiquidacaoDespesaEvento entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_EVENTO_INCLUIR",
                new SqlParameter("@tb_subempenho_cancelamento_id_subempenho_cancelamento", entity.SubempenhoId),
                new SqlParameter("@cd_fonte", entity.Fonte),
                new SqlParameter("@cd_evento", entity.NumeroEvento),
                new SqlParameter("@cd_classificacao", entity.Classificacao),
                new SqlParameter("@ds_inscricao", entity.InscricaoEvento),
                new SqlParameter("@vl_evento", entity.ValorUnitario)
            );
        }

        public string GetTableName()
        {
            return "tb_subempenho_evento";
        }

        public int Save(LiquidacaoDespesaEvento entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_CANCELAMENTO_EVENTO_SALVAR",
                new SqlParameter("@id_subempenho_cancelamento_evento", entity.Id),
                new SqlParameter("@tb_subempenho_cancelamento_id_subempenho_cancelamento", entity.SubempenhoId),
                new SqlParameter("@cd_fonte", entity.Fonte),
                new SqlParameter("@cd_evento", entity.NumeroEvento),
                new SqlParameter("@cd_classificacao", entity.Classificacao),
                new SqlParameter("@ds_inscricao", entity.InscricaoEvento),
                new SqlParameter("@vl_evento", entity.ValorUnitario)
            );
        }
    }
}
