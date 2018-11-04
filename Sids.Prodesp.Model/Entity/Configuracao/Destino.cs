namespace Sids.Prodesp.Model.Entity.Configuracao
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Destino
    {
        [Column("id_destino")]
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Column("cd_destino")]
        public string Codigo { get; set; }

        [Display(Name = "Descrição")]
        [Column("ds_destino")]
        public string Descricao { get; set; }
    }
}
