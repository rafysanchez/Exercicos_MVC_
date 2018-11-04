namespace Sids.Prodesp.Infrastructure.DataBase.LiquidacaoDespesa
{
    using Helpers;
    using Model.Base.LiquidacaoDespesa;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SubempenhoItemDal : ICrudSubempenhoItem
    {
        public int Edit(LiquidacaoDespesaItem entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_ITEM_ALTERAR",
                new SqlParameter("@id_subempenho_item", entity.Id),
                new SqlParameter("@tb_subempenho_id_subempenho", entity.SubempenhoId),
                new SqlParameter("@cd_servico", entity.CodigoItemServico),
                new SqlParameter("@cd_unidade_fornecimento", entity.CodigoUnidadeFornecimentoItem),
                new SqlParameter("@qt_material_servico", entity.QuantidadeMaterialServico),
                new SqlParameter("@cd_status_siafisico", entity.StatusSiafisicoItem),
                new SqlParameter("@nr_sequencia", entity.SequenciaItem)
            );
        }

        public IEnumerable<LiquidacaoDespesaItem> Fetch(LiquidacaoDespesaItem entity)
        {
            return DataHelper.List<SubempenhoItem>("PR_SUBEMPENHO_ITEM_CONSULTAR",
                new SqlParameter("@id_subempenho_item", entity.Id),
                new SqlParameter("@tb_subempenho_id_subempenho", entity.SubempenhoId)
            );
        }

        public int Remove(int id)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_ITEM_EXCLUIR",
                new SqlParameter("@id_subempenho_item", id)
            );
        }

        public int Add(LiquidacaoDespesaItem entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_ITEM_INCLUIR",
                new SqlParameter("@tb_subempenho_id_subempenho", entity.SubempenhoId),
                new SqlParameter("@cd_servico", entity.CodigoItemServico),
                new SqlParameter("@cd_unidade_fornecimento", entity.CodigoUnidadeFornecimentoItem),
                new SqlParameter("@qt_material_servico", entity.QuantidadeMaterialServico),
                new SqlParameter("@cd_status_siafisico", entity.StatusSiafisicoItem)
            );
        }

        public string GetTableName()
        {
            return "tb_subempenho_item";
        }

        public int Save(LiquidacaoDespesaItem entity)
        {
            return DataHelper.Get<int>("PR_SUBEMPENHO_ITEM_SALVAR",
                new SqlParameter("@id_subempenho_item", entity.Id),
                new SqlParameter("@tb_subempenho_id_subempenho", entity.SubempenhoId),
                new SqlParameter("@cd_servico", entity.CodigoItemServico),
                new SqlParameter("@cd_unidade_fornecimento", entity.CodigoUnidadeFornecimentoItem),
                new SqlParameter("@qt_material_servico", entity.QuantidadeMaterialServico),
                new SqlParameter("@cd_status_siafisico", entity.StatusSiafisicoItem),
                new SqlParameter("@nr_sequencia", entity.SequenciaItem),
                new SqlParameter("@transmitir", entity.Transmitir),
                new SqlParameter("@vl_valor", entity.Valor)
            );
        }
    }
}
