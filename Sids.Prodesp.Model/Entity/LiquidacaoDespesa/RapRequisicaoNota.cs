
namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using Base.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations.Schema;
    public class RapRequisicaoNota : LiquidacaoDespesaNota
    {        
        [Column("id_rap_requisicao_nota")]
        public override int Id { get; set; }

        [Column("tb_rap_requisicao_id_rap_requisicao")]
        public override int SubempenhoId { get; set; }
    }
}
