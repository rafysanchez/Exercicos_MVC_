namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SubempenhoCancelamentoEvento : LiquidacaoDespesaEvento
    {
        [Column("id_subempenho_cancelamento_evento")]
        public override int Id { get; set; }

        [Column("tb_subempenho_cancelamento_id_subempenho_cancelamento")]
        public override int SubempenhoId { get; set; }
    }
}
