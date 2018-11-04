namespace Sids.Prodesp.Model.Entity.Reserva
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReservaCancelamentoMes : AbstractMes
    {
        [Display(Name = "Codigo")]
        [Column("id_cancelamento_mes")]
        public override int Codigo { get; set; }
        [Display(Name = "Descricao")]
        [Column("ds_mes")]
        public override string Descricao { get; set; }
        [Display(Name = "Valor Mes")]
        [Column("vr_mes")]
        public override decimal ValorMes { get; set; }
        [Display(Name = "Reforco")]
        [Column("id_cancelamento")]
        public override int Id { get; set; }
    }
}
