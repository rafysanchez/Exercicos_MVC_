namespace Sids.Prodesp.Model.Entity.Reserva
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReservaReforcoMes: AbstractMes
    {
        [Display(Name = "Codigo")]
        [Column("id_reforco_mes")]
        public override int Codigo { get; set; }
        [Display(Name = "Descricao")]
        [Column("ds_mes")]
        public override string Descricao { get; set; }
        [Display(Name = "Valor Mes")]
        [Column("vr_mes")]
        public override decimal ValorMes { get; set; }
        [Display(Name = "Reforco")]
        [Column("id_reforco")]
        public override int Id { get; set; }
    }
}
