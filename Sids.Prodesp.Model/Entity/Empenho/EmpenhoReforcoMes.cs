namespace Sids.Prodesp.Model.Entity.Empenho
{
    using Base.Empenho;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmpenhoReforcoMes : BaseEmpenhoMes
    {
        [Display(Name = "Codigo")]
        [Column("id_empenho_reforco_mes")]
        public override int Codigo { get; set; }

        [Display(Name = "Reforço")]
        [Column("tb_empenho_reforco_id_empenho_reforco")]
        public override int Id { get; set; }
    }
}
