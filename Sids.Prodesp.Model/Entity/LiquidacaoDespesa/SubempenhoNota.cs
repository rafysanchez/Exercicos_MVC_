namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SubempenhoNota : LiquidacaoDespesaNota
    {
        [Column("id_subempenho_nota")]
        public override int Id { get; set; }

        [Column("tb_subempenho_id_subempenho")]
        public override int SubempenhoId { get; set; }
    }
}
