namespace Sids.Prodesp.Model.Base.Empenho
{
    using Interface.Empenho;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEmpenhoItem : IEmpenhoItem
    {
        [Column("id_item")]
        public virtual int Id { get; set; }

        [Column("tb_empenho_id_empenho")]
        public virtual int EmpenhoId { get; set; }

        [Display(Name = "Item de Serviço")]
        [Column("cd_item_servico")]
        public string CodigoItemServico { get; set; }

        [Display(Name = "Unidade de Fornecimento")]
        [Column("cd_unidade_fornecimento")]
        public string CodigoUnidadeFornecimentoItem { get; set; }

        [Display(Name = "Unidade de Medida")]
        [Column("ds_unidade_medida")]
        public string DescricaoUnidadeMedida { get; set; }

        [Display(Name = "Qtd. Material ou Serviço")]
        [Column("qt_material_servico")]
        public decimal QuantidadeMaterialServico { get; set; }

        [Display(Name = "Justificativa")]
        [Column("ds_justificativa_preco")]
        public string DescricaoJustificativaPreco { get; set; }

        [Display(Name = "Descrição")]
        [Column("ds_item_siafem")]
        public string DescricaoItemSiafem { get; set; }

        [Display(Name = "Preço Unitário")]
        [Column("vr_unitario")]
        public decimal ValorUnitario { get; set; }

        [Display(Name = "Preço Total")]
        [Column("vr_preco_total")]
        public decimal ValorTotal { get; set; }

        [Column("ds_status_siafisico_item")]
        public string StatusSiafisicoItem { get; set; }
        
        [Column("nr_sequeincia")]
        public int SequenciaItem { get; set; }
    }
}
