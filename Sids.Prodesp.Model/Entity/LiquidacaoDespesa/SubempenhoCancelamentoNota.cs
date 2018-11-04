namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SubempenhoCancelamentoNota : LiquidacaoDespesaNota
    {
        [Column("id_subempenho_cancelamento_nota")]
        public override int Id { get; set; }

        [Column("tb_subempenho_cancelamento_id_subempenho_cancelamento")]
        public override int SubempenhoId { get; set; }
    }
}
