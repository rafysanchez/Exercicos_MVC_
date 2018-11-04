namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CenarioTipo
    {
        [Column("id_cenario_tipo")]
        public int Id { get; set; }

        [Display(Name = "Tipo Apropriação / Subempenho")]
        [Column("ds_cenario_tipo")]
        public string Descricao { get; set; }
    }
}
