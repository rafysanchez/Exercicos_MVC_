namespace Sids.Prodesp.Model.Entity.Empenho
{
    using Base.Empenho;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmpenhoCancelamentoItem : BaseEmpenhoItem
    {
        [Column("tb_empenho_cancelamento_id_empenho_cancelamento")]
        public override int EmpenhoId { get; set; }
    }
}
