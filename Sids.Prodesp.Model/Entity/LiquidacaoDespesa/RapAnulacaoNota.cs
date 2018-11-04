
namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;
    public class RapAnulacaoNota : LiquidacaoDespesaNota
    {
        [Column("id_rap_anulacao_nota")]
        public override int Id { get; set; }

        [Column("tb_rap_anulacao_id_rap_anulacao")]
        public override int SubempenhoId { get; set; }
    }
}
