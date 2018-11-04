using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Base.LiquidacaoDespesa;

namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    public class RapInscricaoNota : LiquidacaoDespesaNota
    {
        [Column("id_rap_inscricao_nota")]
        public override int Id { get; set; }

        [Column("tb_rap_inscricao_id_rap_inscricao")]
        public override int SubempenhoId { get; set; }
    }
}
