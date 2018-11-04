namespace Sids.Prodesp.Model.Entity.Configuracao
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Mes
    {
        [Column("id_mes")]
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Column("cd_mes")]
        public string Codigo { get; set; }

        [Display(Name = "Descrição")]
        [Column("ds_mes")]
        public string Descricao { get; set; }
    }
}
