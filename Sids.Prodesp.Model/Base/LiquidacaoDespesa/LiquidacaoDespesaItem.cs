namespace Sids.Prodesp.Model.Base.LiquidacaoDespesa
{
    using Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    using Entity.LiquidacaoDespesa;

    public class LiquidacaoDespesaItem : ILiquidacaoDespesaItem
    {
        [Column("id_subempenho_item")]
        public virtual int Id { get; set; }

        [Column("tb_subempenho_id_subempenho")]
        public virtual int SubempenhoId { get; set; }

        [Column("cd_servico")]
        public string CodigoItemServico { get; set; }

        [Column("cd_unidade_fornecimento")]
        public string CodigoUnidadeFornecimentoItem { get; set; }

        [Column("qt_material_servico")]
        public decimal QuantidadeMaterialServico { get; set; }

        [Column("cd_status_siafisico")]
        public string StatusSiafisicoItem { get; set; }

        [Column("nr_sequencia")]
        public int SequenciaItem { get; set; }

        [Column("transmitir")]
        public bool? Transmitir { get; set; }

        public decimal QuantidadeLiquidar { get; set; }

        [Column("vl_valor")]
        public decimal Valor { get; set; }


        public SubempenhoItem ToSubempenhoItem()
        {
            var item = new SubempenhoItem();

            item.Id = this.Id;
            item.SubempenhoId = this.SubempenhoId;
            item.CodigoItemServico = this.CodigoItemServico;
            item.CodigoUnidadeFornecimentoItem = this.CodigoUnidadeFornecimentoItem;
            item.QuantidadeMaterialServico = this.QuantidadeMaterialServico;
            item.StatusSiafisicoItem = this.StatusSiafisicoItem;
            item.SequenciaItem = this.SequenciaItem;
            item.Transmitir = this.Transmitir ?? false;
            item.QuantidadeLiquidar = this.QuantidadeLiquidar;

            return item;
        }
    }
}
