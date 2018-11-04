namespace Sids.Prodesp.Model.Base.LiquidacaoDespesa
{
    using Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiquidacaoDespesaNota : ILiquidacaoDespesaNota
    {
        [Column("id_subempenho_nota")]
        public virtual int Id { get; set; }

        [Column("tb_subempenho_id_subempenho")]
        public virtual int SubempenhoId { get; set; }

        [Column("cd_nota")]
        public string CodigoNotaFiscal { get; set; }

        [Column("nr_ordem")]
        public int Ordem { get; set; }
    }
}
