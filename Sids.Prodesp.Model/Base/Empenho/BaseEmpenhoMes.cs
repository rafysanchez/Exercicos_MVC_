namespace Sids.Prodesp.Model.Base.Empenho
{
    using Interface;
    using Interface.Base;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEmpenhoMes : IMes
    {
        [Display(Name = "Codigo")]
        [Column("id_empenho_mes")]
        public virtual int Codigo { get; set; }

        [Display(Name = "Reserva")]
        [Column("tb_empenho_id_empenho")]
        public virtual int Id { get; set; }

        [Display(Name = "Descricao")]
        [Column("ds_mes")]
        public string Descricao { get; set; }

        [Display(Name = "Valor Mes")]
        [Column("vr_mes")]
        public decimal ValorMes { get; set; }
    }
}
