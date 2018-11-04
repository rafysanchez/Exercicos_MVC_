namespace Sids.Prodesp.Model.Entity.Empenho
{
    using Base.Empenho;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmpenhoCancelamentoMes : BaseEmpenhoMes
    {
        [Display(Name = "Codigo")]
        [Column("id_empenho_cancelamento_mes")]
        public override int Codigo { get; set; }

        [Display(Name = "Cancelamento")]
        [Column("tb_empenho_cancelamento_id_empenho_cancelamento")]
        public override int Id { get; set; }
    }
}
